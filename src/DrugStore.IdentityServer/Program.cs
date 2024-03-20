using DrugStore.IdentityServer;
using DrugStore.Infrastructure.DataProtection;
using DrugStore.Infrastructure.Logging;
using DrugStore.Infrastructure.OpenTelemetry;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.AddOpenTelemetry();
    builder.AddRedisDataProtection();
    builder.AddSerilog(builder.Environment.ApplicationName);

    var app = builder
        .ConfigureServices()
        .ConfigurePipeline();

    app.Run();
}
catch (Exception ex) when (ex is not HostAbortedException)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}