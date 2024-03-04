using System.Reflection;
using System.Security.Claims;
using DrugStore.Application;
using DrugStore.Domain.IdentityAggregate;
using DrugStore.Domain.IdentityAggregate.Helpers;
using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure;
using DrugStore.Persistence;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DrugStore.WebAPI.Extensions;

public static class ProgramExtension
{
    public static void AddIdentity(this IServiceCollection services)
    {
        services.AddAuthentication(IdentityConstants.BearerScheme)
            .AddBearerToken(IdentityConstants.BearerScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);

        services.AddAuthorizationBuilder()
            .AddPolicy(PolicieHelper.Admin,
                policy => policy
                    .RequireRole(RoleHelper.Admin)
                    .RequireClaim(ClaimTypes.Role, ClaimHelper.Read, ClaimHelper.Write, ClaimHelper.Manage))
            .AddPolicy(PolicieHelper.Customer,
                policy => policy
                    .RequireRole(RoleHelper.Customer)
                    .RequireClaim(ClaimTypes.Role, ClaimHelper.Read, ClaimHelper.Write));

        services.AddIdentityCore<ApplicationUser>()
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders()
            .AddApiEndpoints();
    }

    public static void AddInfrastructureService(this IServiceCollection services, WebApplicationBuilder builder) 
        => services.AddInfrastructure(builder);

    public static void AddApplicationService(this IServiceCollection services) => services.AddApplication();

    public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration) 
        => services.AddPostgresDbContext(configuration).AddDatabaseDeveloperPageExceptionFilter();

    public static void UseInfrastructureService(this WebApplication app) => app.UseInfrastructure();

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

        foreach (var endpoint in endpoints) endpoint.MapEndpoint(builder);

        return app;
    }
}