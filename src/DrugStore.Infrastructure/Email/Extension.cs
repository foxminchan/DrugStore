using DrugStore.Infrastructure.Email.SendGrid;
using DrugStore.Infrastructure.Email.SendGrid.Internal;
using DrugStore.Infrastructure.Validator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DrugStore.Infrastructure.Email;

public static class Extension
{
    public static IServiceCollection AddEmail(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<SendGridSettings>()
            .Bind(configuration.GetSection(nameof(SendGridSettings)))
            .ValidateFluentValidation()
            .ValidateOnStart();

        var provider = services.BuildServiceProvider().GetRequiredService<IOptions<SendGridSettings>>().Value;

        services.AddFluentEmail(provider.DefaultFromEmail, "Drug Store")
            .AddSendGridSender(provider.ApiKey)
            .AddRazorRenderer();

        services.AddTransient<IEmailService, EmailService>();

        return services;
    }
}