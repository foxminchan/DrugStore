using Ardalis.GuardClauses;
using DrugStore.Infrastructure.Cache.Redis;
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

        var redisConn = builder.Configuration.GetSection("RedisSettings").Get<RedisSettings>()?.Url;
        Guard.Against.Null(redisConn, message: "Redis URL not found.");

        var identityServer = builder.Configuration.GetValue<string>("IdentityUrlExternal");
        Guard.Against.Null(identityServer, message: "IdentityServer URL not found.");

        var kafka = builder.Configuration.GetValue<string>("KafkaUrl");
        Guard.Against.Null(kafka, message: "Kafka URL not found.");

        builder.Services.AddHealthChecks()
            .AddCheck("self", () => HealthCheckResult.Healthy())
            .AddCheck<LocalFileHealthCheck>(name: "Local", tags: ["storage"])
            .AddNpgSql(postgresConn, name: "Postgres", tags: ["database"])
            .AddRedis(redisConn, "Redis", tags: ["redis"])
            .AddIdentityServer(new Uri(identityServer), name: "Identity Server", tags: ["identity-server"])
            .AddKafka(options => options.BootstrapServers = kafka, name: "Kafka", tags: ["kafka"]);

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

        app.MapHealthChecks("/liveness", new() { Predicate = r => r.Name.Contains("self") });

        app.MapHealthChecksUI(options => options.UIPath = "/hc-ui");
    }
}