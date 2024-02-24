using Ardalis.GuardClauses;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DrugStore.Infrastructure.HealthCheck;

public static class Extension
{
    public static WebApplicationBuilder AddHealthCheck(this WebApplicationBuilder builder)
    {
        var postgresConn = builder.Configuration.GetConnectionString("Postgres");
        Guard.Against.Null(postgresConn, message: "Connection string 'Postgres' not found.");

        var redisConn = builder.Configuration.GetConnectionString("Redis");
        Guard.Against.Null(redisConn, message: "Connection string 'Redis' not found.");

        builder.Services.AddHealthChecks()
            .AddNpgSql(postgresConn, tags: ["database"])
            .AddRedis(redisConn, tags: ["redis"]);

        builder.Services
            .AddHealthChecksUI(options =>
            {
                options.AddHealthCheckEndpoint("Health Check API", "/hc");
                options.SetEvaluationTimeInSeconds(60);
                options.DisableDatabaseMigrations();
            })
            .AddInMemoryStorage();

        return builder;
    }

    public static void MapHealthCheck(this WebApplication app)
    {
        app.MapHealthChecks("/hc",
            new()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
                AllowCachingResponses = false,
                ResultStatusCodes =
                {
                    [HealthStatus.Healthy] = StatusCodes.Status200OK,
                    [HealthStatus.Degraded] = StatusCodes.Status200OK,
                    [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                }
            });

        app.MapHealthChecks("/liveness", new()
        {
            Predicate = r => r.Name.Contains("self")
        });

        app.MapHealthChecksUI(options => options.UIPath = "/hc-ui");
    }
}
