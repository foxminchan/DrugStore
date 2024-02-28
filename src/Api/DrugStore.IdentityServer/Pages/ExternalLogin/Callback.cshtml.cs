using System.Security.Claims;

using DrugStore.Domain.IdentityAggregate;

using Duende.IdentityServer;
using Duende.IdentityServer.Events;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;

using IdentityModel;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DrugStore.IdentityServer.Pages.ExternalLogin;

[AllowAnonymous]
[SecurityHeaders]
public class Callback(
    IIdentityServerInteractionService interaction,
    IEventService events,
    ILogger<Callback> logger,
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager) : PageModel
{
    public async Task<IActionResult> OnGet()
    {
        // read external identity from the temporary cookie
        AuthenticateResult result =
            await HttpContext.AuthenticateAsync(IdentityServerConstants.ExternalCookieAuthenticationScheme);
        if (!result.Succeeded)
        {
            throw new ExternalAuthenticationException();
        }

        ClaimsPrincipal externalUser = result.Principal;

        if (logger.IsEnabled(LogLevel.Debug))
        {
            IEnumerable<string> externalClaims = externalUser.Claims.Select(c => $"{c.Type}: {c.Value}");
            logger.LogDebug("External claims: {@claims}", externalClaims);
        }

        // lookup our user and external provider info
        // try to determine the unique id of the external user (issued by the provider)
        // the most common claim type for that are the sub claim and the NameIdentifier
        // depending on the external provider, some other claim type might be used
        Claim userIdClaim = externalUser.FindFirst(JwtClaimTypes.Subject) ??
                            externalUser.FindFirst(ClaimTypes.NameIdentifier) ??
                            throw new UnknownUserIdException();

        string provider = result.Properties.Items["scheme"];
        string providerUserId = userIdClaim.Value;

        // find external user
        // this might be where you might initiate a custom workflow for user registration
        // in this sample we don't show how that would be done, as our sample implementation
        // simply auto-provisions new external user
        ApplicationUser user = await userManager.FindByLoginAsync(provider, providerUserId) ?? await AutoProvisionUserAsync(provider, providerUserId, externalUser.Claims);

        // this allows us to collect any additional claims or properties
        // for the specific protocols used and store them in the local auth cookie.
        // this is typically used to store data needed for sign out from those protocols.
        List<Claim> additionalLocalClaims = [];
        AuthenticationProperties localSignInProps = new();
        CaptureExternalLoginContext(result, additionalLocalClaims, localSignInProps);

        // issue authentication cookie for user
        await signInManager.SignInWithClaimsAsync(user, localSignInProps, additionalLocalClaims);

        // delete temporary cookie used during external authentication
        await HttpContext.SignOutAsync(IdentityServerConstants.ExternalCookieAuthenticationScheme);

        // retrieve return URL
        string returnUrl = result.Properties.Items["returnUrl"] ?? "~/";

        // check if external login is in the context of an OIDC request
        AuthorizationRequest context = await interaction.GetAuthorizationContextAsync(returnUrl);
        await events.RaiseAsync(new UserLoginSuccessEvent(provider, providerUserId, user.Id.ToString(), user.UserName,
            true,
            context?.Client.ClientId));

        if (context is null)
            return Redirect(returnUrl);

        return context.IsNativeClient()
            ? this.LoadingPage(returnUrl)
            : Redirect(returnUrl);
    }

    private async Task<ApplicationUser> AutoProvisionUserAsync(string provider, string providerUserId,
        IEnumerable<Claim> claims)
    {
        Guid sub = Guid.NewGuid();

        ApplicationUser user = new() { Id = sub, UserName = sub.ToString() };

        // email
        IEnumerable<Claim> enumerable = claims as Claim[] ?? claims.ToArray();
        string email = enumerable.FirstOrDefault(x => x.Type == JwtClaimTypes.Email)?.Value ??
                       enumerable.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        if (email is { })
        {
            user.Email = email;
        }

        // create a list of claims that we want to transfer into our store
        List<Claim> filtered = [];

        // user's display name
        string name = enumerable.FirstOrDefault(x => x.Type == JwtClaimTypes.Name)?.Value ??
                      enumerable.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
        if (name is { })
        {
            filtered.Add(new(JwtClaimTypes.Name, name));
        }
        else
        {
            string first = enumerable.FirstOrDefault(x => x.Type == JwtClaimTypes.GivenName)?.Value ??
                           enumerable.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value;
            string last = enumerable.FirstOrDefault(x => x.Type == JwtClaimTypes.FamilyName)?.Value ??
                          enumerable.FirstOrDefault(x => x.Type == ClaimTypes.Surname)?.Value;
            switch (first)
            {
                case { } when last is { }:
                    filtered.Add(new(JwtClaimTypes.Name, first + " " + last));
                    break;

                case { }:
                    filtered.Add(new(JwtClaimTypes.Name, first));
                    break;

                default:
                    {
                        if (last is { })
                        {
                            filtered.Add(new(JwtClaimTypes.Name, last));
                        }

                        break;
                    }
            }
        }

        IdentityResult identityResult = await userManager.CreateAsync(user);
        if (!identityResult.Succeeded)
            throw new IdentityErrorsException(identityResult.Errors.First().Description);

        if (filtered.Count != 0)
        {
            identityResult = await userManager.AddClaimsAsync(user, filtered);
            if (!identityResult.Succeeded)
                throw new IdentityErrorsException(identityResult.Errors.First().Description);
        }

        identityResult = await userManager.AddLoginAsync(user, new(provider, providerUserId, provider));
        return !identityResult.Succeeded
            ? throw new IdentityErrorsException(identityResult.Errors.First().Description)
            : user;
    }

    // if the external login is OIDC-based, there are certain things we need to preserve to make logout work
    // this will be different for WS-Fed, SAML2p or other protocols
    private static void CaptureExternalLoginContext(AuthenticateResult externalResult, List<Claim> localClaims,
        AuthenticationProperties localSignInProps)
    {
        // capture the idp used to login, so the session knows where the user came from
        localClaims.Add(new(JwtClaimTypes.IdentityProvider, externalResult.Properties?.Items["scheme"] ?? throw new InvalidOperationException()));

        // if the external system sent a session id claim, copy it over
        // so we can use it for single sign-out
        Claim sid = externalResult.Principal?.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.SessionId);
        if (sid is { })
        {
            localClaims.Add(new(JwtClaimTypes.SessionId, sid.Value));
        }

        // if the external provider issued an id_token, we'll keep it for signout
        string idToken = externalResult.Properties.GetTokenValue("id_token");
        if (idToken is { })
        {
            localSignInProps.StoreTokens([new() { Name = "id_token", Value = idToken }]);
        }
    }
}
