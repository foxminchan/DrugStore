// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DrugStore.IdentityServer.Pages.Ciba;

[SecurityHeaders]
[Authorize]
public sealed class AllModel(IBackchannelAuthenticationInteractionService backchannelAuthenticationInteractionService)
    : PageModel
{
    public IEnumerable<BackchannelUserLoginRequest> Logins { get; set; } = default!;

    public async Task OnGet()
        => Logins = await backchannelAuthenticationInteractionService.GetPendingLoginRequestsForCurrentUserAsync();
}