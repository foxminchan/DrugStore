using FluentValidation;

namespace DrugStore.Infrastructure.Email.SendGrid;

public sealed class SendGridValidator : AbstractValidator<SendGridSettings>
{
    public SendGridValidator()
    {
        RuleFor(x => x.ApiKey)
            .NotEmpty();

        RuleFor(x => x.DefaultFromEmail)
            .NotEmpty()
            .EmailAddress();
    }
}