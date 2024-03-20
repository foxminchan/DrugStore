using DrugStore.IdentityServer.Constants;
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
        new(Claims.Read, "Read Access to API"),
        new(Claims.Write, "Write Access to API"),
        new(Claims.Manage, "Manage Access to API")
    ];

    public static IEnumerable<ApiResource> ApiResources =>
    [
        new()
        {
            Name = "drugstore",
            DisplayName = "DrugStore API",
            Scopes = { Claims.Read, Claims.Write, Claims.Manage }
        }
    ];

    public static IEnumerable<Client> Clients(IConfiguration configuration) =>
    [  new()
        {
            ClientId = "ro.client",
            ClientName = "Resource Owner Client",
            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
            ClientSecrets = { new Secret("secret".Sha256()) },
            AllowedScopes = { Claims.Read, Claims.Write, Claims.Manage }
        },
        new()
        {
            ClientId = "storefront",
            ClientName = "Storefront Client",
            ClientSecrets = { new Secret("secret".Sha256()) },
            AllowedGrantTypes = GrantTypes.Code,
            RequirePkce = true,
            RequireClientSecret = false,
            RequireConsent = false,
            AllowedCorsOrigins = { configuration["ClientUrl:Storefront"] ?? throw new InvalidOperationException() },
            RedirectUris = { $"{configuration["ClientUrl:Storefront"]}/authentication/login-callback" },
            PostLogoutRedirectUris = { $"{configuration["ClientUrl:Storefront"]}/authentication/logout-callback" },
            AllowedScopes =
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                IdentityServerConstants.StandardScopes.OfflineAccess,
                Claims.Read,
                Claims.Write
            }
        },
        new()
        {
            ClientId = "backoffice",
            ClientName = "Backoffice Client",
            ClientSecrets = { new Secret("secret".Sha256()) },
            AllowedGrantTypes = GrantTypes.Code,
            RequirePkce = true,
            RequireClientSecret = false,
            RequireConsent = false,
            AllowedCorsOrigins = { configuration["ClientUrl:Backoffice"] ?? throw new InvalidOperationException() },
            RedirectUris = { $"{configuration["ClientUrl:Backoffice"]}/authentication/login-callback" },
            PostLogoutRedirectUris = { $"{configuration["ClientUrl:Backoffice"]}/authentication/logout-callback" },
            AllowedScopes =
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                IdentityServerConstants.StandardScopes.OfflineAccess,
                Claims.Read,
                Claims.Write,
                Claims.Manage
            }
        },
        new()
        {
            ClientId = "apiswaggerui",
            ClientName = "DrugStore API",
            ClientSecrets = { new Secret("secret".Sha256()) },
            AllowedGrantTypes = GrantTypes.Implicit,
            AllowAccessTokensViaBrowser = true,
            RequirePkce = true,
            AllowedCorsOrigins = { configuration["ClientUrl:Swagger"] ?? throw new InvalidOperationException() },
            RedirectUris = { $"{configuration["ClientUrl:Swagger"]}/swagger/oauth2-redirect.html" },
            PostLogoutRedirectUris = { $"{configuration["ClientUrl:Swagger"]}/swagger/" },
            AllowedScopes =
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                Claims.Read,
                Claims.Write,
                Claims.Manage
            }
        }
    ];
}