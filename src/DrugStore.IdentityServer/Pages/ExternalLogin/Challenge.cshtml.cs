using DrugStore.IdentityServer.Pages.Account.Login;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DrugStore.IdentityServer.Pages.ExternalLogin;

[AllowAnonymous]
[SecurityHeaders]
public class Challenge(IIdentityServerInteractionService interactionService) : PageModel
{
    public IActionResult OnGet(string scheme, string returnUrl)
    {
        if (string.IsNullOrEmpty(returnUrl)) returnUrl = "~/";

        // validate returnUrl - either it is a valid OIDC URL or back to a local page
        if (Url.IsLocalUrl(returnUrl) && interactionService.IsValidReturnUrl(returnUrl))
            // user might have clicked on a malicious link - should be logged
            throw new InvalidUrlException();

        // start challenge and roundtrip the return URL and scheme
        AuthenticationProperties props = new()
        {
            RedirectUri = Url.Page("/externallogin/callback"),
            Items = { { "returnUrl", returnUrl }, { "scheme", scheme } }
        };

        return Challenge(props, scheme);
    }
}