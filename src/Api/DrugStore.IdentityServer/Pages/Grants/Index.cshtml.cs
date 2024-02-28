using System.ComponentModel.DataAnnotations;

using Duende.IdentityServer.Events;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Stores;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DrugStore.IdentityServer.Pages.Grants;

[SecurityHeaders]
[Authorize]
public class Index(
    IIdentityServerInteractionService interaction,
    IClientStore clients,
    IResourceStore resources,
    IEventService events) : PageModel
{
    public ViewModel View { get; set; }

    [BindProperty][Required] public string ClientId { get; set; }

    public async Task OnGet()
    {
        IEnumerable<Grant> grants = await interaction.GetAllUserGrantsAsync();

        List<GrantViewModel> list = [];
        foreach (Grant grant in grants)
        {
            Client client = await clients.FindClientByIdAsync(grant.ClientId);
            if (client is null)
            {
                continue;
            }

            Resources resources1 = await resources.FindResourcesByScopeAsync(grant.Scopes);

            GrantViewModel item = new()
            {
                ClientId = client.ClientId,
                ClientName = client.ClientName ?? client.ClientId,
                ClientLogoUrl = client.LogoUri,
                ClientUrl = client.ClientUri,
                Description = grant.Description,
                Created = grant.CreationTime,
                Expires = grant.Expiration,
                IdentityGrantNames = resources1.IdentityResources.Select(x => x.DisplayName ?? x.Name).ToArray(),
                ApiGrantNames = resources1.ApiScopes.Select(x => x.DisplayName ?? x.Name).ToArray()
            };

            list.Add(item);
        }

        View = new() { Grants = list };
    }

    public async Task<IActionResult> OnPost()
    {
        await interaction.RevokeUserConsentAsync(ClientId);
        await events.RaiseAsync(new GrantsRevokedEvent(User.GetSubjectId(), ClientId));

        return RedirectToPage("/Grants/Index");
    }
}
