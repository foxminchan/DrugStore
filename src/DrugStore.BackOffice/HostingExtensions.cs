﻿using DrugStore.BackOffice.Components.Pages.Categories.Services;
using DrugStore.BackOffice.Components.Pages.Orders.Services;
using DrugStore.BackOffice.Components.Pages.Products.Services;
using DrugStore.BackOffice.Components.Pages.Users.Shared.Services;
using DrugStore.BackOffice.Delegates;
using DrugStore.Domain.IdentityAggregate.Constants;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Refit;

namespace DrugStore.BackOffice;

public static class HostingExtensions
{
    public static void RegisterApis(this IServiceCollection services, string apiRoute)
    {
        services.AddTransient<RetryDelegate>();
        services.AddTransient<LoggingDelegate>();

        var apis = new List<Type>
        {
            typeof(ICategoriesApi),
            typeof(IProductsApi),
            typeof(IOrdersApi),
            typeof(IUserApi)
        };

        foreach (var apiType in apis)
        {
            services.AddRefitClient(apiType)
                .ConfigureHttpClient(c => c.BaseAddress = new(apiRoute))
                .AddHttpMessageHandler<LoggingDelegate>()
                .AddHttpMessageHandler<RetryDelegate>();
        }
    }

    public static void AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
            {
                options.Authority = configuration["IdentityServer:Authority"];
                options.RequireHttpsMetadata = false;
                options.GetClaimsFromUserInfoEndpoint = true;

                options.ClientId = configuration["IdentityServer:ClientId"];
                options.ClientSecret = "secret";
                options.ResponseType = OpenIdConnectResponseType.Code;

                options.SaveTokens = true;

                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("offline_access");
                options.Scope.Add(Claims.READ);
                options.Scope.Add(Claims.WRITE);
                options.Scope.Add(Claims.MANAGE);

                options.TokenValidationParameters = new()
                {
                    NameClaimType = "name",
                    RoleClaimType = "role"
                };
            });

        services.AddAuthorization();
    }
}