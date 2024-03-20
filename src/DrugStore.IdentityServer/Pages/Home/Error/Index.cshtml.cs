// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DrugStore.IdentityServer.Pages.Home.Error;

[AllowAnonymous]
[SecurityHeaders]
public sealed class Index(IIdentityServerInteractionService interaction, IWebHostEnvironment environment)
    : PageModel
{
    public ViewModel View { get; set; } = new();

    public async Task OnGet(string? errorId)
    {
        // retrieve error details from identityserver
        var message = await interaction.GetErrorContextAsync(errorId);
        if (message is not null)
        {
            View.Error = message;

            if (!environment.IsDevelopment())
                // only show in development
                message.ErrorDescription = null;
        }
    }
}