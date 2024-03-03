using DrugStore.Infrastructure.Email.Abstractions;
using FluentEmail.Core;
using Microsoft.Extensions.Logging;
using Polly;

namespace DrugStore.Infrastructure.Email.SendGrid.Internal;

public sealed class EmailService(
    IFluentEmailFactory fluentEmailFactory,
    ILogger<EmailService> logger) : IEmailService
{
    public async Task SendEmailAsync(EmailMetadata metadata)
        => await Policy
            .Handle<System.Exception>()
            .WaitAndRetryAsync(
                3, _ => TimeSpan.FromMilliseconds(new Random().Next(1000, 3000)),
                (_, retryCount, _) =>
                    logger.LogWarning("Failed to send email. Retrying... Attempt: {retryCount}", retryCount)
            )
            .ExecuteAsync(async () =>
            {
                var email = fluentEmailFactory
                    .Create()
                    .To(metadata.To)
                    .Subject(metadata.Subject)
                    .UsingTemplateFromFile(metadata.Template, metadata.Model);

                await email.SendAsync();
            });
}