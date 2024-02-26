using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DrugStore.IdentityServer.Pages.Home.Error;

[AllowAnonymous]
[SecurityHeaders]
public class Index : PageModel
{
    private readonly IWebHostEnvironment _environment;
    private readonly IIdentityServerInteractionService _interaction;

    public Index(IIdentityServerInteractionService interaction, IWebHostEnvironment environment)
    {
        _interaction = interaction;
        _environment = environment;
    }

    public ViewModel View { get; set; }

    public async Task OnGet(string errorId)
    {
        View = new();

        // retrieve error details from identityserver
        ErrorMessage message = await _interaction.GetErrorContextAsync(errorId);
        if (message != null)
        {
            View.Error = message;

            if (!_environment.IsDevelopment())
            {
                // only show in development
                message.ErrorDescription = null;
            }
        }
    }
}
