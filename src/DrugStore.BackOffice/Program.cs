using Ardalis.GuardClauses;
using DrugStore.BackOffice.Categories;
using DrugStore.BackOffice.Components;
using DrugStore.BackOffice.Customer;
using DrugStore.BackOffice.Products;
using DrugStore.Infrastructure.Logging;
using DrugStore.Infrastructure.OpenTelemetry;
using Radzen;
using Refit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddRadzenComponents();

builder.AddOpenTelemetry(builder.Configuration);
builder.AddSerilog(builder.Environment.ApplicationName);

var apiRoute = builder.Configuration.GetSection("ApiUrl").Value;

Guard.Against.NullOrWhiteSpace(apiRoute);

builder.Services.AddRefitClient<ICategoriesApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new(apiRoute));

builder.Services.AddRefitClient<IProductsApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new(apiRoute));

builder.Services.AddRefitClient<ICustomersApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new(apiRoute));

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
