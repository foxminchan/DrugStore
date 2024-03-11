using Ardalis.GuardClauses;
using DrugStore.BackOffice;
using DrugStore.BackOffice.Components;
using DrugStore.BackOffice.Components.Pages.Categories;
using DrugStore.BackOffice.Components.Pages.Customers;
using DrugStore.BackOffice.Components.Pages.Orders;
using DrugStore.BackOffice.Components.Pages.Products;
using DrugStore.BackOffice.Components.Pages.Users;
using DrugStore.BackOffice.Services;
using DrugStore.Infrastructure.Logging;
using DrugStore.Infrastructure.OpenTelemetry;
using Radzen;
using Refit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddRadzenComponents();

builder.AddOpenTelemetry();
builder.AddSerilog(builder.Environment.ApplicationName);

builder.Services.AddScoped(typeof(ExportService<>));

builder.Services.AddOptions<Settings>()
    .Bind(builder.Configuration.GetSection(nameof(Settings)))
    .ValidateDataAnnotations()
    .ValidateOnStart();

var apiRoute = builder.Configuration.GetSection(nameof(Settings)).Get<Settings>()?.ApiEndpoint;

Guard.Against.NullOrWhiteSpace(apiRoute);

builder.Services.AddRefitClient<ICategoriesApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new(apiRoute));

builder.Services.AddRefitClient<IProductsApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new(apiRoute));

builder.Services.AddRefitClient<ICustomersApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new(apiRoute));

builder.Services.AddRefitClient<IOrdersApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new(apiRoute));

builder.Services.AddRefitClient<IUsersApi>()
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
