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
public class Index : PageModel
{
    private readonly IClientStore _clients;
    private readonly IEventService _events;
    private readonly IIdentityServerInteractionService _interaction;
    private readonly IResourceStore _resources;

    public Index(IIdentityServerInteractionService interaction,
        IClientStore clients,
        IResourceStore resources,
        IEventService events)
    {
        _interaction = interaction;
        _clients = clients;
        _resources = resources;
        _events = events;
    }

    public ViewModel View { get; set; }

    [BindProperty] [Required] public string ClientId { get; set; }

    public async Task OnGet()
    {
        IEnumerable<Grant> grants = await _interaction.GetAllUserGrantsAsync();

        List<GrantViewModel> list = new();
        foreach (Grant grant in grants)
        {
            Client client = await _clients.FindClientByIdAsync(grant.ClientId);
            if (client != null)
            {
                Resources resources = await _resources.FindResourcesByScopeAsync(grant.Scopes);

                GrantViewModel item = new()
                {
                    ClientId = client.ClientId,
                    ClientName = client.ClientName ?? client.ClientId,
                    ClientLogoUrl = client.LogoUri,
                    ClientUrl = client.ClientUri,
                    Description = grant.Description,
                    Created = grant.CreationTime,
                    Expires = grant.Expiration,
                    IdentityGrantNames = resources.IdentityResources.Select(x => x.DisplayName ?? x.Name).ToArray(),
                    ApiGrantNames = resources.ApiScopes.Select(x => x.DisplayName ?? x.Name).ToArray()
                };

                list.Add(item);
            }
        }

        View = new() { Grants = list };
    }

    public async Task<IActionResult> OnPost()
    {
        await _interaction.RevokeUserConsentAsync(ClientId);
        await _events.RaiseAsync(new GrantsRevokedEvent(User.GetSubjectId(), ClientId));

        return RedirectToPage("/Grants/Index");
    }
}
