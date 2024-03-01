using DrugStore.Infrastructure.HealthCheck;
using DrugStore.Infrastructure.Logging;
using DrugStore.Infrastructure.OpenTelemetry;

var builder = WebApplication.CreateBuilder(args);

builder.AddHealthCheck();
builder.Logging.AddJsonConsole();
builder.AddOpenTelemetry(builder.Configuration);
builder.AddSerilog(builder.Environment.ApplicationName);

var app = builder.Build();

app.MapHealthCheck();
app.Map("/", () => Results.Redirect("/hc-ui"));
app.MapPrometheusScrapingEndpoint();
app.Run();