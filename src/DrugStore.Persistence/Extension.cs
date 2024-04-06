using Ardalis.GuardClauses;
using DrugStore.Domain.SharedKernel;
using DrugStore.Persistence.CompiledModels;
using DrugStore.Persistence.Interceptors;
using DrugStore.Persistence.Repositories;
using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using Serilog;

namespace DrugStore.Persistence;

public static class Extension
{
    public static IServiceCollection AddPostgresDbContext(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Postgres");

        Guard.Against.Null(connectionString, message: "Connection string 'Postgres' not found.");

        services.AddSingleton<AuditableEntityInterceptor>();

        services.AddDbContextPool<ApplicationDbContext>((sp, options) =>
        {
            options.UseNpgsql(connectionString, sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(AssemblyReference.DbContextAssembly.FullName);
                    sqlOptions.EnableRetryOnFailure(15, TimeSpan.FromSeconds(30),
                        null);
                })
                .UseExceptionProcessor()
                .EnableServiceProviderCaching()
                .UseSnakeCaseNamingConvention()
                .UseModel(ApplicationDbContextModel.Instance)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            options.AddInterceptors(sp.GetRequiredService<AuditableEntityInterceptor>());

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development)
                options
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging();
        });

        services.AddScoped<IDatabaseFacade>(p => p.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IDomainEventContext>(p => p.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<ApplicationDbContextInitializer>();
        services.AddScoped(typeof(IReadRepository<>), typeof(Repository<>));
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        return services;
    }

    public static void ApplyDatabaseMigration(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var retryPolicy = CreateRetryPolicy(app.Configuration, Log.Logger);
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        retryPolicy.Execute(context.Database.Migrate);
    }

    private static Policy CreateRetryPolicy(IConfiguration configuration, ILogger logger)
    {
        if (bool.TryParse(configuration["RetryMigrations"], out _))
            return Policy.Handle<Exception>().WaitAndRetryForever(
                _ => TimeSpan.FromSeconds(5),
                (exception, retry, _) =>
                {
                    logger.Warning(
                        exception,
                        "[{Prefix}] Exception {ExceptionType} with message {Message} detected during database migration (retry attempt {Retry}, connection {Connection})",
                        nameof(ApplyDatabaseMigration),
                        exception.GetType().Name,
                        exception.Message,
                        retry,
                        configuration.GetConnectionString("Postgres"));
                }
            );

        return Policy.NoOp();
    }
}