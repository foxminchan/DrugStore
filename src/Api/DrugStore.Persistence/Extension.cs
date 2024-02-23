using Ardalis.GuardClauses;
using DrugStore.Persistence.Interceptors;
using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DrugStore.Persistence;

public static class Extension
{
    public static IServiceCollection AddPostgresDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

        services.AddScoped<IDbCommandInterceptor, TimingInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContextPool<ApplicationDbContext>((sp, options) =>
        {
            options.UseNpgsql(connectionString, sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(AssemblyReference.DomainAssembly.FullName);
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorCodesToAdd: null);
                })
                .UseExceptionProcessor()
                .EnableServiceProviderCaching()
                .UseSnakeCaseNamingConvention()
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            options.AddInterceptors(sp.GetServices<IDbCommandInterceptor>());
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development)
                options
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging();
        });

        services.AddScoped<IDatabaseFacade>(p => p.GetRequiredService<ApplicationDbContext>());
        services.AddScoped(typeof(Repository<>));

        return services;
    }
}
