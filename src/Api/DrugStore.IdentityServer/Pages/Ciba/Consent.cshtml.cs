using Duende.IdentityServer;
using Duende.IdentityServer.Events;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DrugStore.IdentityServer.Pages.Ciba;

[Authorize]
[SecurityHeaders]
public class Consent(
    IBackchannelAuthenticationInteractionService interaction,
    IEventService events,
    ILogger<Consent> logger) : PageModel
{
    public ViewModel View { get; set; }

    [BindProperty] public InputModel Input { get; set; }

    public async Task<IActionResult> OnGet(string id)
    {
        View = await BuildViewModelAsync(id);
        if (View == null)
        {
            return RedirectToPage("/Home/Error/Index");
        }

        Input = new() { Id = id };

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        // validate return url is still valid
        BackchannelUserLoginRequest request = await interaction.GetLoginRequestByInternalIdAsync(Input.Id);
        if (request == null || request.Subject.GetSubjectId() != User.GetSubjectId())
        {
            logger.LogError("Invalid id {id}", Input.Id);
            return RedirectToPage("/Home/Error/Index");
        }

        CompleteBackchannelLoginRequest result = null;

        switch (Input?.Button)
        {
            // user clicked 'no' - send back the standard 'access_denied' response
            case "no":
                result = new(Input.Id);

                // emit event
                await events.RaiseAsync(new ConsentDeniedEvent(User.GetSubjectId(), request.Client.ClientId,
                    request.ValidatedResources.RawScopeValues));
                break;
            // user clicked 'yes' - validate the data
            // if the user consented to some scope, build the response model
            case "yes" when Input.ScopesConsented != null && Input.ScopesConsented.Any():
                {
                    IEnumerable<string> scopes = Input.ScopesConsented;
                    if (ConsentOptions.EnableOfflineAccess == false)
                    {
                        scopes = scopes.Where(x => x != IdentityServerConstants.StandardScopes.OfflineAccess);
                    }

                    result = new(Input.Id) { ScopesValuesConsented = scopes.ToArray(), Description = Input.Description };

                    // emit event
                    await events.RaiseAsync(new ConsentGrantedEvent(User.GetSubjectId(), request.Client.ClientId,
                        request.ValidatedResources.RawScopeValues, result.ScopesValuesConsented, false));
                    break;
                }
            case "yes":
                ModelState.AddModelError("", ConsentOptions.MustChooseOneErrorMessage);
                break;
            default:
                ModelState.AddModelError("", ConsentOptions.InvalidSelectionErrorMessage);
                break;
        }

        if (result != null)
        {
            // communicate outcome of consent back to identityserver
            await interaction.CompleteLoginRequestAsync(result);

            return RedirectToPage("/Ciba/All");
        }

        // we need to redisplay the consent UI
        View = await BuildViewModelAsync(Input?.Id, Input);
        return Page();
    }

    private async Task<ViewModel> BuildViewModelAsync(string id, InputModel model = null)
    {
        BackchannelUserLoginRequest request = await interaction.GetLoginRequestByInternalIdAsync(id);
        if (request != null && request.Subject.GetSubjectId() == User.GetSubjectId())
        {
            return CreateConsentViewModel(model, id, request);
        }

        logger.LogError("No backchannel login request matching id: {id}", id);
        return null;
    }

    private ViewModel CreateConsentViewModel(
        InputModel model, string id,
        BackchannelUserLoginRequest request)
    {
        ViewModel vm = new()
        {
            ClientName = request.Client.ClientName ?? request.Client.ClientId,
            ClientUrl = request.Client.ClientUri,
            ClientLogoUrl = request.Client.LogoUri,
            BindingMessage = request.BindingMessage,
            IdentityScopes = request.ValidatedResources.Resources.IdentityResources
                .Select(x => CreateScopeViewModel(x,
                    model?.ScopesConsented == null || model.ScopesConsented?.Contains(x.Name) == true))
                .ToArray()
        };

        IEnumerable<string> resourceIndicators = request.RequestedResourceIndicators ?? Enumerable.Empty<string>();
        IEnumerable<ApiResource> apiResources =
            request.ValidatedResources.Resources.ApiResources.Where(x => resourceIndicators.Contains(x.Name));

        List<ScopeViewModel> apiScopes = [];
        foreach (ParsedScopeValue parsedScope in request.ValidatedResources.ParsedScopes)
        {
            ApiScope apiScope = request.ValidatedResources.Resources.FindApiScope(parsedScope.ParsedName);
            if (apiScope is null)
            {
                continue;
            }

            ScopeViewModel scopeVm = CreateScopeViewModel(parsedScope, apiScope,
                model == null || model.ScopesConsented?.Contains(parsedScope.RawValue) == true);
            scopeVm.Resources = apiResources.Where(x => x.Scopes.Contains(parsedScope.ParsedName))
                .Select(x => new ResourceViewModel { Name = x.Name, DisplayName = x.DisplayName ?? x.Name })
                .ToArray();
            apiScopes.Add(scopeVm);
        }

        if (ConsentOptions.EnableOfflineAccess && request.ValidatedResources.Resources.OfflineAccess)
        {
            apiScopes.Add(GetOfflineAccessScope(model == null ||
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
