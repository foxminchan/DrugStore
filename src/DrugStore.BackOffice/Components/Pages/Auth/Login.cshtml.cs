using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DrugStore.BackOffice.Components.Pages.Auth;

public sealed class LoginModel : PageModel
{
    public async Task OnGetAsync(string returnUrl)
    {
        if (HttpContext.User.Identity is not null && !HttpContext.User.Identity.IsAuthenticated)
        {
            await HttpContext.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme, new()
            {
                RedirectUri = Url.IsLocalUrl(returnUrl) ? returnUrl : "~/"
            });
        }
        else
        {
            Response.Redirect(Url.Content("~/"));
        }
    }
}