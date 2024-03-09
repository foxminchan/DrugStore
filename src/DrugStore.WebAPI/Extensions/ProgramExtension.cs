using System.Security.Claims;
using DrugStore.Application;
using DrugStore.Domain.IdentityAggregate;
using DrugStore.Domain.IdentityAggregate.Helpers;
using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure;
using DrugStore.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DrugStore.WebAPI.Extensions;

public static class ProgramExtension
{
    public static void AddIdentity(this IHostApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Audience = nameof(DrugStore).ToLowerInvariant();
                options.Authority = builder.Configuration.GetValue<string>("IdentityUrl");
                options.RequireHttpsMetadata = false;
            });

        builder.Services.AddAuthorizationBuilder()
            .AddPolicy(PolicieHelper.Admin,
                policy => policy
                    .RequireRole(RoleHelper.Admin)
                    .RequireClaim(ClaimTypes.Role, ClaimHelper.Read, ClaimHelper.Write, ClaimHelper.Manage))
            .AddPolicy(PolicieHelper.Customer,
                policy => policy
                    .RequireRole(RoleHelper.Customer)
                    .RequireClaim(ClaimTypes.Role, ClaimHelper.Read, ClaimHelper.Write));

        builder.Services.AddIdentityCore<ApplicationUser>()
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");
    }

    public static void MapIdentity(this WebApplication app)
    {
        app.UseAntiforgery();
        app.UseAuthentication();
        app.UseAuthorization();
    }

    public static void AddInfrastructureService(this IServiceCollection services, WebApplicationBuilder builder)
        => services.AddInfrastructure(builder);

    public static void AddApplicationService(this IHostApplicationBuilder builder) => builder.Services.AddApplication();

    public static IServiceCollection AddCustomDbContext(this IHostApplicationBuilder builder)
        => builder.Services.AddPostgresDbContext(builder.Configuration).AddDatabaseDeveloperPageExceptionFilter();

    public static void UseInfrastructureService(this WebApplication app) => app.UseInfrastructure();

    public static IServiceCollection AddCustomCors(this IHostApplicationBuilder builder, string corsName = "api")
    {
        var clientEndpoints = new[]
        {
            builder.Configuration.GetValue<string>("ClientEndpoints:StoreFront") ?? string.Empty,
            builder.Configuration.GetValue<string>("ClientEndpoints:BackOffice") ?? string.Empty
        };

        builder.Services.AddCors(options => options.AddPolicy(corsName,
            policy => policy
                .WithOrigins(clientEndpoints)
                .AllowAnyHeader()
                .AllowAnyMethod()
        ));

        return builder.Services;
    }

    public static IApplicationBuilder UseCustomCors(this IApplicationBuilder app, string corsName = "api")
        => app.UseCors(corsName);

    public static IServiceCollection AddEndpoints(this IHostApplicationBuilder builder)
    {
        var serviceDescriptors = AssemblyReference.Program
            .DefinedTypes
            .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                           type.IsAssignableTo(typeof(IEndpoint)))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
            .ToArray();

        builder.Services.TryAddEnumerable(serviceDescriptors);

        return builder.Services;
    }

    public static IApplicationBuilder MapEndpoints(this WebApplication app, RouteGroupBuilder? routeGroupBuilder = null)
    {
        var endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();
        IEndpointRouteBuilder builder = routeGroupBuilder is null ? app : routeGroupBuilder;

        foreach (var endpoint in endpoints) endpoint.MapEndpoint(builder);

        return app;
    }
}