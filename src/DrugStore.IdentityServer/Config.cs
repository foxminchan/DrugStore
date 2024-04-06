using DrugStore.Domain.IdentityAggregate.Constants;
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
        new(Claims.READ, "Read Access to API"),
        new(Claims.WRITE, "Write Access to API"),
        new(Claims.MANAGE, "Manage Access to API")
    ];

    public static IEnumerable<ApiResource> ApiResources =>
    [
        new()
        {
            Name = "api.drugstore",
            DisplayName = "Drug Store Api",
            Scopes = { Claims.READ, Claims.WRITE, Claims.MANAGE }
        }
    ];

    public static IEnumerable<Client> Clients(IConfiguration configuration) =>
    [
        new()
        {
            ClientId = "ro.client",
            ClientName = "Resource Owner Client",
            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
            ClientSecrets = { new Secret("secret".Sha256()) },
            AllowedScopes = { Claims.READ, Claims.WRITE, Claims.MANAGE }
        },
        new()
        {
            ClientId = "storefront",
            ClientName = "Storefront Client",
            ClientSecrets = { new Secret("secret".Sha256()) },
            AllowedGrantTypes = GrantTypes.Code,
            RequireConsent = false,
            RequirePkce = true,
            RedirectUris = { $"{configuration["ClientUrl:Storefront"]}/signin-oidc" },
            PostLogoutRedirectUris = { $"{configuration["ClientUrl:Storefront"]}/signout-callback-oidc" },
            AllowedScopes =
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                Claims.READ, Claims.WRITE, Claims.MANAGE
            }
        },
        new()
        {
            ClientId = "backoffice",
            ClientName = "Backoffice Client",
            ClientSecrets = { new Secret("secret".Sha256()) },
            AllowedGrantTypes = GrantTypes.Code,
            RequireConsent = false,
            RequirePkce = true,
            RedirectUris = { $"{configuration["ClientUrl:Backoffice"]}/signin-oidc" },
            PostLogoutRedirectUris = { $"{configuration["ClientUrl:Backoffice"]}/signout-callback-oidc" },
            AllowedScopes =
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                Claims.READ, Claims.WRITE, Claims.MANAGE
            }
        },
        new()
        {
            ClientId = "apiswaggerui",
            ClientName = "Drug Store API",
            ClientSecrets = { new Secret("secret".Sha256()) },
            AllowedGrantTypes = GrantTypes.Code,
            RequireConsent = false,
            RequirePkce = true,
            RedirectUris = { $"{configuration["ClientUrl:Swagger"]}/swagger/oauth2-redirect.html" },
            PostLogoutRedirectUris = { $"{configuration["ClientUrl:Swagger"]}/swagger/oauth2-redirect.html" },
            AllowedCorsOrigins = { configuration["ClientUrl:Swagger"] ?? throw new InvalidOperationException() },
            AllowedScopes =
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                Claims.READ, Claims.WRITE, Claims.MANAGE
            }
        }
    ];
}