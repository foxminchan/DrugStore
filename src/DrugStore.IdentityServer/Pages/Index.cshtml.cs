using System.Reflection;
using Duende.IdentityServer.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DrugStore.IdentityServer.Pages;

[AllowAnonymous]
public class Index : PageModel
{
    public string Version { get; private set; } = string.Empty;

    public void OnGet() 
        => Version = typeof(IdentityServerMiddleware).Assembly
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion.Split('+')[0];
}