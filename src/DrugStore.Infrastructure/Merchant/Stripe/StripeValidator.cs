using FluentValidation;

namespace DrugStore.Infrastructure.Merchant.Stripe;

public sealed class StripeValidator : AbstractValidator<StripeSettings>
{
    public StripeValidator() => RuleFor(x => x.ApiKey).NotEmpty();
}