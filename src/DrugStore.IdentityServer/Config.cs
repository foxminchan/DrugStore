using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace DrugStore.IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
    [
        new IdentityResources.OpenId(),
        new IdentityResources.Profile()
    ];

    public static IEnumerable<ApiScope> ApiScopes =>
    [
        new("read", "Read Access to API"),
        new("write", "Write Access to API"),
        new("manage", "Manage Access to API")
    ];

    public static IEnumerable<ApiResource> ApiResources =>
    [
        new() { Name = "drugstore", DisplayName = "DrugStore API", Scopes = { "read", "write", "manage" } }
    ];

    public static IEnumerable<Client> Clients(IConfiguration configuration) =>
    [
        new()
        {
            ClientId = "storefront",
            ClientName = "Storefront Client",
            AllowedGrantTypes = GrantTypes.Code,
            RequirePkce = true,
            RequireClientSecret = false,
            RequireConsent = false,
            AllowedCorsOrigins = { configuration["StorefrontClient"] },
            RedirectUris = { $"{configuration["StorefrontClient"]}/authentication/login-callback" },
            PostLogoutRedirectUris = { $"{configuration["StorefrontClient"]}/authentication/logout-callback" },
            AllowedScopes =
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                "read",
                "write"
            }
        },
        new()
        {
            ClientId = "backoffice",
            ClientName = "Backoffice Client",
            AllowedGrantTypes = GrantTypes.Code,
            RequirePkce = true,
            RequireClientSecret = false,
            RequireConsent = false,
            AllowedCorsOrigins = { configuration["BackofficeClient"] },
            RedirectUris = { $"{configuration["BackofficeClient"]}/authentication/login-callback" },
            PostLogoutRedirectUris = { $"{configuration["BackofficeClient"]}/authentication/logout-callback" },
            AllowedScopes =
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                "read",
                "write",
                "manage"
            }
        },
        new()
        {
            ClientId = "apiswaggerui",
            ClientName = "DrugStore API",
            AllowedGrantTypes = GrantTypes.Implicit,
            AllowAccessTokensViaBrowser = true,
            RedirectUris = { $"{configuration["ApiSwaggerUI"]}/swagger/oauth2-redirect.html" },
            PostLogoutRedirectUris = { $"{configuration["ApiSwaggerUI"]}/swagger/" },
            AllowedScopes = { "read", "write", "manage" }
        }
    ];
}