using DrugStore.Infrastructure.Email.Abstractions;

namespace DrugStore.Infrastructure.Email.SendGrid;

public interface IEmailService
{
    Task SendEmailAsync(EmailMetadata metadata);
}