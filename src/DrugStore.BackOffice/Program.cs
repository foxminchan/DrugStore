using Ardalis.GuardClauses;
using DrugStore.BackOffice;
using DrugStore.BackOffice.Components;
using DrugStore.BackOffice.Components.Pages.Categories;
using DrugStore.BackOffice.Components.Pages.Customers;
using DrugStore.BackOffice.Components.Pages.Orders;
using DrugStore.BackOffice.Components.Pages.Products;
using DrugStore.BackOffice.Components.Pages.Users;
using DrugStore.BackOffice.Services;
using DrugStore.Domain.IdentityAggregate.Helpers;
using DrugStore.Infrastructure.Logging;
using DrugStore.Infrastructure.OpenTelemetry;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
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

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
    })
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
    {
        options.Authority = builder.Configuration["IdentityServer:Authority"];
        options.RequireHttpsMetadata = false;
        options.GetClaimsFromUserInfoEndpoint = true;

        options.ClientId = builder.Configuration["IdentityServer:ClientId"];
        options.ClientSecret = "secret";
        options.ResponseType = OpenIdConnectResponseType.Code;

        options.SaveTokens = true;

        options.Scope.Add("openid");
        options.Scope.Add("profile");
        options.Scope.Add("offline_access");
        options.Scope.Add(ClaimHelper.Read);
        options.Scope.Add(ClaimHelper.Write);
        options.Scope.Add(ClaimHelper.Manage);

        options.TokenValidationParameters = new()
        {
            NameClaimType = "name",
            RoleClaimType = "role"
        };
    });

builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapPrometheusScrapingEndpoint();

app.Run();