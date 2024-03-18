using DrugStore.Domain.IdentityAggregate;
using Duende.IdentityServer;
using Duende.IdentityServer.Events;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DrugStore.IdentityServer.Pages.Account.Login;

[SecurityHeaders]
[AllowAnonymous]
public class Index(
    IIdentityServerInteractionService interaction,
    IAuthenticationSchemeProvider schemeProvider,
    IIdentityProviderStore identityProviderStore,
    IEventService events,
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager) : PageModel
{
    public ViewModel View { get; set; }

    [BindProperty] public InputModel Input { get; set; }

    public async Task<IActionResult> OnGet(string returnUrl)
    {
        await BuildModelAsync(returnUrl);

        return View.IsExternalLoginOnly
            ? RedirectToPage("/ExternalLogin/Challenge", new { scheme = View.ExternalLoginScheme, returnUrl })
            : Page();
    }

    public async Task<IActionResult> OnPost()
    {
        // check if we are in the context of an authorization request
        var context = await interaction.GetAuthorizationContextAsync(Input.ReturnUrl);

        if (Input.Button == "register")
            return Redirect("~/Account/Register?ReturnUrl=" + Uri.EscapeDataString(Input.ReturnUrl));

        // the user clicked the "cancel" button
        if (Input.Button != "login" && Input.Button != "register")
        {
            if (context is null) return Redirect("~/");

            // if the user cancels, send a result back into IdentityServer as if they
            // denied the consent (even if this client does not require consent).
            // this will send back an access denied OIDC error response to the client.
            await interaction.DenyAuthorizationAsync(context, AuthorizationError.AccessDenied);

            // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
            return context.IsNativeClient()
                ?
                // The client is native, so this change in how to
                // return the response is for better UX for the end user.
                this.LoadingPage(Input.ReturnUrl)
                : Redirect(Input.ReturnUrl);

            // since we don't have a valid context, then we just go back to the home page
        }

        if (ModelState.IsValid)
        {
            var result =
                await signInManager.PasswordSignInAsync(Input.Username, Input.Password, Input.RememberLogin, true);
            if (result.Succeeded)
            {
                var user = await userManager.FindByNameAsync(Input.Username);
                await events.RaiseAsync(new UserLoginSuccessEvent(user?.UserName, user!.Id.ToString(), user!.UserName,
                    clientId: context?.Client.ClientId));

                if (context is not null)
                    return context.IsNativeClient()
                        ?
                        // The client is native, so this change in how to
                        // return the response is for better UX for the end user.
                        this.LoadingPage(Input.ReturnUrl)
                        :
                        // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
                        Redirect(Input.ReturnUrl);

                // request for a local page
                if (Url.IsLocalUrl(Input.ReturnUrl)) return Redirect(Input.ReturnUrl);

                if (string.IsNullOrEmpty(Input.ReturnUrl)) return Redirect("~/");

                // user might have clicked on a malicious link - should be logged
                throw new InvalidUrlException();
            }

            await events.RaiseAsync(new UserLoginFailureEvent(Input.Username, "invalid credentials",
                clientId: context?.Client.ClientId));
            ModelState.AddModelError(string.Empty, LoginOptions.InvalidCredentialsErrorMessage);
        }

        // something went wrong, show form with error
        await BuildModelAsync(Input.ReturnUrl);
        return Page();
    }

    private async Task BuildModelAsync(string returnUrl)
    {
        Input = new() { ReturnUrl = returnUrl };

        var context = await interaction.GetAuthorizationContextAsync(returnUrl);
        if (context?.IdP is not null && await schemeProvider.GetSchemeAsync(context.IdP) is not null)
        {
            var local = context.IdP == IdentityServerConstants.LocalIdentityProvider;

            // this is meant to short circuit the UI and only trigger the one external IdP
            View = new() { EnableLocalLogin = local };

            Input.Username = context.LoginHint;

            if (!local)
                View.ExternalProviders =
                    [new() { AuthenticationScheme = context.IdP }];

            return;
        }

        var schemes = await schemeProvider.GetAllSchemesAsync();

        var providers = schemes
            .Where(x => x.DisplayName is not null)
            .Select(x => new ViewModel.ExternalProvider
            {
                DisplayName = x.DisplayName ?? x.Name, AuthenticationScheme = x.Name
            }).ToList();

        var dynamicSchemes = (await identityProviderStore.GetAllSchemeNamesAsync())
            .Where(x => x.Enabled)
            .Select(x => new ViewModel.ExternalProvider
            {
                AuthenticationScheme = x.Scheme, DisplayName = x.DisplayName
            });
        providers.AddRange(dynamicSchemes);

        var allowLocal = true;
        var client = context?.Client;
        if (client is not null)
        {
            allowLocal = client.EnableLocalLogin;
            if (client.IdentityProviderRestrictions.Count != 0)
                providers = providers.Where(provider =>
                    client.IdentityProviderRestrictions.Contains(provider.AuthenticationScheme)).ToList();
        }

        View = new()
        {
            AllowRememberLogin = LoginOptions.AllowRememberLogin,
            EnableLocalLogin = allowLocal && LoginOptions.AllowLocalLogin,
            ExternalProviders = [.. providers]
        };
    }
}