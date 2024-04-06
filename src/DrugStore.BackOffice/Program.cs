using Ardalis.GuardClauses;
using Ardalis.ListStartupServices;
using DrugStore.BackOffice;
using DrugStore.BackOffice.Components;
using DrugStore.Infrastructure.DataProtection;
using DrugStore.Infrastructure.Logging;
using DrugStore.Infrastructure.OpenTelemetry;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddHubOptions(options => options.MaximumReceiveMessageSize = 10 * 1024 * 1024);

builder.AddRedisDataProtection();
builder.Services.AddRadzenComponents();
builder.Services.AddControllers();

builder.AddOpenTelemetry();
builder.AddSerilog(builder.Environment.ApplicationName);

builder.Services.AddOptions<Settings>()
    .Bind(builder.Configuration.GetSection(nameof(Settings)))
    .ValidateDataAnnotations()
    .ValidateOnStart();

var apiRoute = builder.Configuration.GetSection(nameof(Settings)).Get<Settings>()?.ApiEndpoint;

Guard.Against.NullOrWhiteSpace(apiRoute);

builder.Services.RegisterApis(apiRoute);
builder.Services.AddCustomAuthentication(builder.Configuration);
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.Configure<ServiceConfig>(config => config.Services = [.. builder.Services]);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", true);
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseShowAllServicesMiddleware();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapPrometheusScrapingEndpoint();

app.Run();