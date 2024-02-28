using DrugStore.IdentityServer;
using DrugStore.Infrastructure.Logging;
using DrugStore.Infrastructure.OpenTelemetry;

using Serilog;

try
{
    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

    builder.AddSerilog(builder.Environment.ApplicationName);
    builder.AddOpenTelemetry(builder.Configuration);

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
