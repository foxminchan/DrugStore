using DrugStore.IdentityServer;
using DrugStore.Infrastructure.Cache;
using DrugStore.Infrastructure.Logging;
using DrugStore.Infrastructure.OpenTelemetry;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.AddRedisCache();
    builder.AddOpenTelemetry();
    builder.AddSerilog(builder.Environment.ApplicationName);

    var app = builder
        .ConfigureServices()
        .ConfigurePipeline();

    app.MapPrometheusScrapingEndpoint();
    app.Run();
}
catch (Exception ex) when (
    ex.GetType().Name is not "StopTheHostException"
    && ex.GetType().Name is not "HostAbortedException"
)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}