using DrugStore.Infrastructure.Merchant.Stripe;
using DrugStore.Infrastructure.Merchant.Stripe.Internal;
using DrugStore.Infrastructure.Validator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Stripe;

namespace DrugStore.Infrastructure.Merchant;

public static class Extension
{
    public static IServiceCollection AddMerchant(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<StripeSettings>()
            .Bind(configuration.GetSection(nameof(StripeSettings)))
            .ValidateFluentValidation()
            .ValidateOnStart();

        services.AddScoped<CustomerService>();
        services.AddScoped<ChargeService>();
        services.AddScoped<TokenService>();

        StripeConfiguration.ApiKey = services.BuildServiceProvider()
            .GetRequiredService<IOptions<StripeSettings>>().Value.ApiKey;

        services.AddScoped<IStripeService, StripeService>();

        return services;
    }
}