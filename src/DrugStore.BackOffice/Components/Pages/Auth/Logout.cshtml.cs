using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DrugStore.BackOffice.Components.Pages.Auth;

public sealed class LogoutModel : PageModel
{
    public async Task<IActionResult> OnGetAsync()
        => SignOut(
            new AuthenticationProperties { RedirectUri = "~/" },
            CookieAuthenticationDefaults.AuthenticationScheme,
            OpenIdConnectDefaults.AuthenticationScheme
        );
}