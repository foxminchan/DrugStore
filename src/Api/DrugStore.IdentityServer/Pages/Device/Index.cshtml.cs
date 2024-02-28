using DrugStore.IdentityServer.Pages.Consent;

using Duende.IdentityServer;
using Duende.IdentityServer.Events;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Validation;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DrugStore.IdentityServer.Pages.Device;

[SecurityHeaders]
[Authorize]
public class Index(
    IDeviceFlowInteractionService interaction,
    IEventService eventService) : PageModel
{
    public ViewModel View { get; set; }

    [BindProperty] public InputModel Input { get; set; }

    public async Task<IActionResult> OnGet(string userCode)
    {
        if (string.IsNullOrWhiteSpace(userCode))
        {
            View = new();
            Input = new();
            return Page();
        }

        View = await BuildViewModelAsync(userCode);
        if (View is null)
        {
            ModelState.AddModelError("", DeviceOptions.InvalidUserCode);
            View = new();
            Input = new();
            return Page();
        }

        Input = new() { UserCode = userCode };

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        DeviceFlowAuthorizationRequest request = await interaction.GetAuthorizationContextAsync(Input.UserCode);
        if (request is null)
        {
            return RedirectToPage("/Home/Error/Index");
        }

        ConsentResponse grantedConsent = null;

        switch (Input.Button)
        {
            // user clicked 'no' - send back the standard 'access_denied' response
            case "no":
                grantedConsent = new() { Error = AuthorizationError.AccessDenied };

                // emit event
                await eventService.RaiseAsync(new ConsentDeniedEvent(User.GetSubjectId(), request.Client.ClientId,
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
                    await eventService.RaiseAsync(new ConsentGrantedEvent(User.GetSubjectId(), request.Client.ClientId,
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
            await interaction.HandleRequestAsync(Input.UserCode, grantedConsent);

            // indicate that's it ok to redirect back to authorization endpoint
            return RedirectToPage("/Device/Success");
        }

        // we need to redisplay the consent UI
        View = await BuildViewModelAsync(Input.UserCode, Input);
        return Page();
    }

    private async Task<ViewModel> BuildViewModelAsync(string userCode, InputModel model = null)
    {
        DeviceFlowAuthorizationRequest request = await interaction.GetAuthorizationContextAsync(userCode);
        return request is { } ? CreateConsentViewModel(model, request) : null;
    }

    private ViewModel CreateConsentViewModel(InputModel model, DeviceFlowAuthorizationRequest request)
    {
        ViewModel vm = new()
        {
            ClientName = request.Client.ClientName ?? request.Client.ClientId,
            ClientUrl = request.Client.ClientUri,
            ClientLogoUrl = request.Client.LogoUri,
            AllowRememberConsent = request.Client.AllowRememberConsent,
            IdentityScopes = request.ValidatedResources.Resources.IdentityResources.Select(x =>
                CreateScopeViewModel(x, model is null || model.ScopesConsented?.Contains(x.Name) == true)).ToArray()
        };

        List<ScopeViewModel> apiScopes = [];
        apiScopes.AddRange(from parsedScope in request.ValidatedResources.ParsedScopes let apiScope = request.ValidatedResources.Resources.FindApiScope(parsedScope.ParsedName) where apiScope is { } select CreateScopeViewModel(parsedScope, apiScope, model is null || model.ScopesConsented?.Contains(parsedScope.RawValue) == true));

        if (DeviceOptions.EnableOfflineAccess && request.ValidatedResources.Resources.OfflineAccess)
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
            Value = identity.Name,
            DisplayName = identity.DisplayName ?? identity.Name,
            Description = identity.Description,
            Emphasize = identity.Emphasize,
            Required = identity.Required,
            Checked = check || identity.Required
        };

    public ScopeViewModel CreateScopeViewModel(ParsedScopeValue parsedScopeValue, ApiScope apiScope, bool check) =>
        new()
        {
            Value = parsedScopeValue.RawValue,
            DisplayName = apiScope.DisplayName ?? apiScope.Name,
            Description = apiScope.Description,
            Emphasize = apiScope.Emphasize,
            Required = apiScope.Required,
            Checked = check || apiScope.Required
        };

    private static ScopeViewModel GetOfflineAccessScope(bool check) =>
        new()
        {
            Value = IdentityServerConstants.StandardScopes.OfflineAccess,
            DisplayName = DeviceOptions.OfflineAccessDisplayName,
            Description = DeviceOptions.OfflineAccessDescription,
            Emphasize = true,
            Checked = check
        };
}
