using Ardalis.GuardClauses;
using DrugStore.BackOffice.Components;
using DrugStore.BackOffice.Components.Pages.Categories;
using DrugStore.BackOffice.Components.Pages.Customers;
using DrugStore.BackOffice.Components.Pages.Products;
using DrugStore.BackOffice.Services;
using DrugStore.Infrastructure.Logging;
using DrugStore.Infrastructure.OpenTelemetry;
using DrugStore.Infrastructure.Validator;
using Radzen;
using Refit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddRadzenComponents();

builder.AddOpenTelemetry();
builder.AddSerilog(builder.Environment.ApplicationName);

builder.Services.AddValidator();

builder.Services.AddScoped(typeof(ExportService<>));

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
