using System.Reflection;
using System.Security.Claims;
using DrugStore.Application;
using DrugStore.Domain.Identity;
using DrugStore.Domain.Identity.Constants;
using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure;
using DrugStore.Persistence;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DrugStore.Presentation.Extensions;

public static class ProgramExtension
{
    public static void AddIdentity(this IServiceCollection services)
    {
        services.AddAuthentication()
            .AddBearerToken(IdentityConstants.BearerScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);

        services.AddAuthorizationBuilder()
            .AddPolicy(Roles.Admin,
                policy => policy
                    .RequireRole(Policies.Admin)
                    .RequireClaim(ClaimTypes.Role,  Claims.Read, Claims.Write, Claims.Manage))
            .AddPolicy(Roles.Customer,
                policy => policy
                    .RequireRole(Policies.Customer)
                    .RequireClaim(ClaimTypes.Role, Claims.Read, Claims.Write));

        services.AddIdentityCore<ApplicationUser>()
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddApiEndpoints();
    }

    public static void AddInfrastructureService(this IServiceCollection services, WebApplicationBuilder builder)
        => services.AddInfrastructure(builder);

    public static void AddApplicationService(this IServiceCollection services)
        => services.AddApplication();

    public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        => services.AddPostgresDbContext(configuration).AddDatabaseDeveloperPageExceptionFilter();

    public static void UseInfrastructureService(this WebApplication app)
        => app.UseInfrastructure();

    public static IServiceCollection AddCustomCors(this IServiceCollection services, string corsName = "api")
        => services.AddCors(options => options.AddPolicy(corsName,
                policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

    public static IApplicationBuilder UseCustomCors(this IApplicationBuilder app, string corsName = "api")
        => app.UseCors(corsName);

    public static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly assembly)
    {
        var serviceDescriptors = assembly
            .DefinedTypes
            .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                           type.IsAssignableTo(typeof(IEndpoint)))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
            .ToArray();

        services.TryAddEnumerable(serviceDescriptors);

        return services;
    }

    public static IApplicationBuilder MapEndpoints(this WebApplication app, RouteGroupBuilder? routeGroupBuilder = null)
    {
        var endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();
        IEndpointRouteBuilder builder = routeGroupBuilder is null ? app : routeGroupBuilder;

        foreach (IEndpoint endpoint in endpoints)
            endpoint.MapEndpoint(builder);

        return app;
    }
}
