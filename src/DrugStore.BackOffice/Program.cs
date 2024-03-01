using DrugStore.BackOffice.Components;
using DrugStore.Infrastructure.Logging;
using DrugStore.Infrastructure.OpenTelemetry;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.AddOpenTelemetry(builder.Configuration);
builder.AddSerilog(builder.Environment.ApplicationName);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapPrometheusScrapingEndpoint();

app.Run();
