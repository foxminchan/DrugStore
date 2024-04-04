using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.BackOffice.Components.Pages.Auth;

public sealed class LoginModel : PageModel
{
    public async Task<IActionResult> OnGetAsync(string returnUrl)
    {
        if (string.IsNullOrWhiteSpace(returnUrl))
        {
            returnUrl = Url.Content("~/");
        }

        if (HttpContext.User.Identity!.IsAuthenticated)
        {
            Response.Redirect(returnUrl);
        }

        return Challenge(
            new AuthenticationProperties
            {
                RedirectUri = returnUrl
            },
            OpenIdConnectDefaults.AuthenticationScheme);
    }
}