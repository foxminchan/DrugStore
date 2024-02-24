using Ardalis.GuardClauses;
using DrugStore.Domain.SharedKernel;
using DrugStore.Persistence.CompileModels;
using DrugStore.Persistence.Interceptors;
using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DrugStore.Persistence;

public static class Extension
{
    public static IServiceCollection AddPostgresDbContext(this IServiceCollection services,
        IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("Postgres");

        Guard.Against.Null(connectionString, message: "Connection string 'Postgres' not found.");

        services.AddScoped<IDbCommandInterceptor, TimingInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContextPool<ApplicationDbContext>((sp, options) =>
        {
            options.UseNpgsql(connectionString, sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(AssemblyReference.DomainAssembly.FullName);
                    sqlOptions.EnableRetryOnFailure(15, TimeSpan.FromSeconds(30),
                        null);
                })
                .UseExceptionProcessor()
                .EnableServiceProviderCaching()
                .UseSnakeCaseNamingConvention()
                .UseModel(ApplicationDbContextModel.Instance)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            options.AddInterceptors(sp.GetServices<IDbCommandInterceptor>());
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development)
            {
                options
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging();
            }
        });

        services.AddScoped<IDatabaseFacade>(p => p.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IDomainEventContext>(p => p.GetRequiredService<ApplicationDbContext>());
        services.AddScoped(typeof(Repository<>));

        return services;
    }
}
