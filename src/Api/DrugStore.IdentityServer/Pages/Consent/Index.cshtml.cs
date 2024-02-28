using Duende.IdentityServer;
using Duende.IdentityServer.Events;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Validation;

using IdentityModel;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DrugStore.IdentityServer.Pages.Consent;

[Authorize]
[SecurityHeaders]
public class Index(
    IIdentityServerInteractionService interaction,
    IEventService events,
    ILogger<Index> logger) : PageModel
{
    public ViewModel View { get; set; }

    [BindProperty] public InputModel Input { get; set; }

    public async Task<IActionResult> OnGet(string returnUrl)
    {
        View = await BuildViewModelAsync(returnUrl);
        if (View is null)
            return RedirectToPage("/Home/Error/Index");

        Input = new() { ReturnUrl = returnUrl };

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        // validate return url is still valid
        AuthorizationRequest request = await interaction.GetAuthorizationContextAsync(Input.ReturnUrl);
        if (request is null)
            return RedirectToPage("/Home/Error/Index");

        ConsentResponse grantedConsent = null;

        switch (Input?.Button)
        {
            // user clicked 'no' - send back the standard 'access_denied' response
            case "no":
                grantedConsent = new() { Error = AuthorizationError.AccessDenied };

                // emit event
                await events.RaiseAsync(new ConsentDeniedEvent(User.GetSubjectId(), request.Client.ClientId,
                    request.ValidatedResources.RawScopeValues));
                break;
            // user clicked 'yes' - validate the data
            // if the user consented to some scope, build the response model
            case "yes" when Input.ScopesConsented is { } && Input.ScopesConsented.Any():
                {
                    IEnumerable<string> scopes = Input.ScopesConsented;
                    if (ConsentOptions.EnableOfflineAccess)
                    {
                        scopes = scopes.Where(x => x != IdentityServerConstants.StandardScopes.OfflineAccess);
                    }

                    grantedConsent = new()
                    {
                        RememberConsent = Input.RememberConsent,
                        ScopesValuesConsented = scopes.ToArray(),
                        Description = Input.Description
                    };

                    // emit event
                    await events.RaiseAsync(new ConsentGrantedEvent(User.GetSubjectId(), request.Client.ClientId,
                        request.ValidatedResources.RawScopeValues, grantedConsent.ScopesValuesConsented,
                        grantedConsent.RememberConsent));
                    break;
                }
            case "yes":
                ModelState.AddModelError("", ConsentOptions.MustChooseOneErrorMessage);
                break;

            default:
                ModelState.AddModelError("", ConsentOptions.InvalidSelectionErrorMessage);
                break;
        }

        if (grantedConsent is { })
        {
            // communicate outcome of consent back to identityserver
            await interaction.GrantConsentAsync(request, grantedConsent);

            // redirect back to authorization endpoint
            return request.IsNativeClient() ?
                // The client is native, so this change in how to
                // return the response is for better UX for the end user.
                this.LoadingPage(Input.ReturnUrl) : Redirect(Input.ReturnUrl);
        }

        // we need to redisplay the consent UI
        View = await BuildViewModelAsync(Input?.ReturnUrl, Input);
        return Page();
    }

    private async Task<ViewModel> BuildViewModelAsync(string returnUrl, InputModel model = null)
    {
        AuthorizationRequest request = await interaction.GetAuthorizationContextAsync(returnUrl);
        if (request is { })
        {
            return CreateConsentViewModel(model, returnUrl, request);
        }

        logger.LogError("No consent request matching request: {returnUrl}", returnUrl);
        return null;
    }

    private ViewModel CreateConsentViewModel(
        InputModel model, string returnUrl,
        AuthorizationRequest request)
    {
        ViewModel vm = new()
        {
            ClientName = request.Client.ClientName ?? request.Client.ClientId,
            ClientUrl = request.Client.ClientUri,
            ClientLogoUrl = request.Client.LogoUri,
            AllowRememberConsent = request.Client.AllowRememberConsent,
            IdentityScopes = request.ValidatedResources.Resources.IdentityResources
            .Select(x => CreateScopeViewModel(x,
                model?.ScopesConsented is null || model.ScopesConsented?.Contains(x.Name) == true))
            .ToArray()
        };

        logger.LogDebug("Request for consent: {returnUrl}", returnUrl);

        IEnumerable<string> resourceIndicators =
            request.Parameters.GetValues(OidcConstants.AuthorizeRequest.Resource) ?? Enumerable.Empty<string>();
        List<ApiResource> apiResources =
            request.ValidatedResources.Resources.ApiResources.Where(x => resourceIndicators.Contains(x.Name))
            .ToList();

        List<ScopeViewModel> apiScopes = [];
        foreach (ParsedScopeValue parsedScope in request.ValidatedResources.ParsedScopes)
        {
            ApiScope apiScope = request.ValidatedResources.Resources.FindApiScope(parsedScope.ParsedName);
            if (apiScope is null)
                continue;

            ScopeViewModel scopeVm = CreateScopeViewModel(parsedScope, apiScope,
                model is null || model.ScopesConsented?.Contains(parsedScope.RawValue) == true);
            scopeVm.Resources = apiResources.Where(x => x.Scopes.Contains(parsedScope.ParsedName))
                .Select(x => new ResourceViewModel { Name = x.Name, DisplayName = x.DisplayName ?? x.Name })
                .ToArray();
            apiScopes.Add(scopeVm);
        }

        if (ConsentOptions.EnableOfflineAccess && request.ValidatedResources.Resources.OfflineAccess)
        {
            apiScopes.Add(GetOfflineAccessScope(model is null ||
                                                model.ScopesConsented?.Contains(IdentityServerConstants.StandardScopes
                                                    .OfflineAccess) == true));
        }

        vm.ApiScopes = apiScopes;

        return vm;
    }

    private static ScopeViewModel CreateScopeViewModel(IdentityResource identity, bool check) =>
        new()
        {
            Name = identity.Name,
            Value = identity.Name,
            DisplayName = identity.DisplayName ?? identity.Name,
            Description = identity.Description,
            Emphasize = identity.Emphasize,
            Required = identity.Required,
            Checked = check || identity.Required
        };

    public ScopeViewModel CreateScopeViewModel(ParsedScopeValue parsedScopeValue, ApiScope apiScope, bool check)
    {
        string displayName = apiScope.DisplayName ?? apiScope.Name;
        if (!string.IsNullOrWhiteSpace(parsedScopeValue.ParsedParameter))
        {
            displayName += ":" + parsedScopeValue.ParsedParameter;
        }

        return new()
        {
            Name = parsedScopeValue.ParsedName,
            Value = parsedScopeValue.RawValue,
            DisplayName = displayName,
            Description = apiScope.Description,
            Emphasize = apiScope.Emphasize,
            Required = apiScope.Required,
            Checked = check || apiScope.Required
        };
    }

    private static ScopeViewModel GetOfflineAccessScope(bool check) =>
        new()
        {
            Value = IdentityServerConstants.StandardScopes.OfflineAccess,
            DisplayName = ConsentOptions.OfflineAccessDisplayName,
            Description = ConsentOptions.OfflineAccessDescription,
            Emphasize = true,
            Checked = check
        };
}
