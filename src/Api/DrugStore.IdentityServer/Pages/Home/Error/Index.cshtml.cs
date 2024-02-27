using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DrugStore.IdentityServer.Pages.Home.Error;

[AllowAnonymous]
[SecurityHeaders]
public class Index(IIdentityServerInteractionService interaction, IWebHostEnvironment environment)
    : PageModel
{
    public ViewModel View { get; set; }

    public async Task OnGet(string errorId)
    {
        View = new();

        // retrieve error details from identityserver
        ErrorMessage message = await interaction.GetErrorContextAsync(errorId);
        if (message is { })
        {
            View.Error = message;

            if (!environment.IsDevelopment())
            {
                // only show in development
                message.ErrorDescription = null;
            }
        }
    }
}
