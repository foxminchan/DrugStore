using DrugStore.Domain.Identity;
using Duende.IdentityServer.Events;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.IdentityServer.Pages.Account.Logout;

[SecurityHeaders]
[AllowAnonymous]
public class Index(
    SignInManager<ApplicationUser> signInManager,
    IIdentityServerInteractionService interaction,
    IEventService events) : PageModel
{
    [BindProperty] public string LogoutId { get; set; }

    public async Task<IActionResult> OnGet(string logoutId)
    {
        LogoutId = logoutId;

        bool showLogoutPrompt = LogoutOptions.ShowLogoutPrompt;

        if (User.Identity is { IsAuthenticated: false })
        {
            // if the user is not authenticated, then just show logged out page
            showLogoutPrompt = false;
        }
        else
        {
            LogoutRequest context = await interaction.GetLogoutContextAsync(LogoutId);
            if (context.ShowSignoutPrompt)
            {
                // it's safe to automatically sign-out
                showLogoutPrompt = false;
            }
        }

        if (showLogoutPrompt)
        {
            // if the request for logout was properly authenticated from IdentityServer, then
            // we don't need to show the prompt and can just log the user out directly.
            return await OnPost();
        }

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (User.Identity is { IsAuthenticated: false })
        {
            return RedirectToPage("/Account/Logout/LoggedOut", new { logoutId = LogoutId });
        }

        // if there's no current logout context, we need to create one
        // this captures necessary info from the current logged in user
        // this can still return null if there is no context needed
        LogoutId ??= await interaction.CreateLogoutContextAsync();

        // delete local authentication cookie
        await signInManager.SignOutAsync();

        // raise the logout event
        await events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));

        // see if we need to trigger federated logout
        string idp = User.FindFirst(JwtClaimTypes.IdentityProvider)?.Value;

        // if it's a local login we can ignore this workflow
        if (idp is null || idp == IdentityServerConstants.LocalIdentityProvider ||
            !await HttpContext.GetSchemeSupportsSignOutAsync(idp))
        {
            return RedirectToPage("/Account/Logout/LoggedOut", new { logoutId = LogoutId });
        }

        // we need to see if the provider supports external logout
        // build a return URL so the upstream provider will redirect back
        // to us after the user has logged out. this allows us to then
        // complete our single sign-out processing.
        string url = Url.Page("/Account/Logout/Loggedout", new { logoutId = LogoutId });

        // this triggers a redirect to the external provider for sign-out
        return SignOut(new AuthenticationProperties { RedirectUri = url }, idp);
    }
}
