using DrugStore.Domain.IdentityAggregate.Helpers;
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
        new(ClaimHelper.Read, "Read Access to API"),
        new(ClaimHelper.Write, "Write Access to API"),
        new(ClaimHelper.Manage, "Manage Access to API")
    ];

    public static IEnumerable<ApiResource> ApiResources =>
    [
        new()
        {
            Name = "drugstore",
            DisplayName = "DrugStore API",
            Scopes = { ClaimHelper.Read, ClaimHelper.Write, ClaimHelper.Manage }
        }
    ];

    public static IEnumerable<Client> Clients(IConfiguration configuration) =>
    [  new()
        {
            ClientId = "ro.client",
            ClientName = "Resource Owner Client",
            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
            ClientSecrets = { new Secret("secret".Sha256()) },
            AllowedScopes = { ClaimHelper.Read, ClaimHelper.Write, ClaimHelper.Manage }
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
            AllowedCorsOrigins = { configuration["StorefrontClient"] },
            RedirectUris = { $"{configuration["StorefrontClient"]}/authentication/login-callback" },
            PostLogoutRedirectUris = { $"{configuration["StorefrontClient"]}/authentication/logout-callback" },
            AllowedScopes =
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                IdentityServerConstants.StandardScopes.OfflineAccess,
                ClaimHelper.Read,
                ClaimHelper.Write
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
            AllowedCorsOrigins = { configuration["BackofficeClient"] },
            RedirectUris = { $"{configuration["BackofficeClient"]}/authentication/login-callback" },
            PostLogoutRedirectUris = { $"{configuration["BackofficeClient"]}/authentication/logout-callback" },
            AllowedScopes =
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                IdentityServerConstants.StandardScopes.OfflineAccess,
                ClaimHelper.Read,
                ClaimHelper.Write,
                ClaimHelper.Manage
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
            RedirectUris = { $"{configuration["ApiSwaggerUI"]}/swagger/oauth2-redirect.html" },
            PostLogoutRedirectUris = { $"{configuration["ApiSwaggerUI"]}/swagger/" },
            AllowedScopes =
            {
                ClaimHelper.Read,
                ClaimHelper.Write,
                ClaimHelper.Manage
            }
        }
    ];
}