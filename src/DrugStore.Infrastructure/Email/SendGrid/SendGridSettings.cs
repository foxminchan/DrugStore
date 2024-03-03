namespace DrugStore.Infrastructure.Email.SendGrid;

public sealed class SendGridSettings
{
    public string ApiKey { get; set; } = string.Empty;
    public string DefaultFromEmail { get; set; } = string.Empty;
}