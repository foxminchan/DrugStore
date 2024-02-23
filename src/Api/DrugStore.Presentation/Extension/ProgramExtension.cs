using DrugStore.Persistence;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using DrugStore.Domain.Identity;
using System.Security.Claims;
using DrugStore.Domain.Identity.Constants;

namespace DrugStore.Presentation.Extension;

public static class ProgramExtension
{
    public static void AddApplicationIdentity(this IServiceCollection services)
    {
        services.AddAuthentication()
            .AddBearerToken(IdentityConstants.BearerScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);

        services.AddAuthorizationBuilder()
            .AddPolicy(Roles.Admin,
                policy => policy
                    .RequireRole(Policies.Admin)
                    .RequireClaim(ClaimTypes.Role, Claims.Create, Claims.Read, Claims.Update, Claims.Delete))
            .AddPolicy(Roles.Customer,
                policy => policy
                    .RequireRole(Policies.Customer)
                    .RequireClaim(ClaimTypes.Role, Claims.Create, Claims.Read));

        services.AddIdentityCore<ApplicationUser>()
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddApiEndpoints();
    }

    public static void MapIdentity(this WebApplication app)
    {
        app.UseAuthentication().UseAuthorization();
        app
            .MapGroup("/api/v1/auth")
            .MapIdentityApi<ApplicationUser>()
            .WithTags("Auth");
    }
}
