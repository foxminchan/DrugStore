using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace DrugStore.Infrastructure.OpenTelemetry;

public static class Extension
{
    public static void AddOpenTelemetry(this IHostApplicationBuilder builder)
    {
        var resourceBuilder = ResourceBuilder
            .CreateDefault()
            .AddService(
                builder.Environment.ApplicationName,
                serviceVersion: "unknown",
                serviceInstanceId: Environment.MachineName
            );

        Uri oltpEndpoint = new(
            builder.Configuration.GetValue<string>("OtlpEndpoint")
            ?? throw new InvalidOperationException("Endpoint is not configured")
        );

        builder.Logging.AddOpenTelemetry(logging =>
            logging.SetResourceBuilder(resourceBuilder)
                .AddOtlpExporter());

        builder.Services.Configure<OpenTelemetryLoggerOptions>(opt =>
        {
            opt.IncludeScopes = true;
            opt.ParseStateValues = true;
            opt.IncludeFormattedMessage = true;
        });

        builder.Services.AddOpenTelemetry()
            .WithTracing(trace =>
                trace.SetResourceBuilder(resourceBuilder)
                    .SetSampler(new AlwaysOnSampler())
                    .AddOtlpExporter(options => options.Endpoint = oltpEndpoint)
                    .AddSource("Microsoft.AspNetCore", "System.Net.Http")
                    .AddEntityFrameworkCoreInstrumentation(b => b.SetDbStatementForText = true)
                    .AddRedisInstrumentation()
                    .AddNpgsql()
            )
            .WithMetrics(meter =>
                meter.SetResourceBuilder(resourceBuilder)
                    .AddPrometheusExporter()
                    .AddOtlpExporter(options => options.Endpoint = oltpEndpoint)
                    .AddMeter("Microsoft.AspNetCore.Hosting", "Microsoft.AspNetCore.Server.Kestrel", "System.Net.Http")
            );
    }
}