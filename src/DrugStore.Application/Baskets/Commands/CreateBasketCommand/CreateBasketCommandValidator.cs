using DrugStore.Persistence.Constants;
using FluentValidation;

namespace DrugStore.Application.Baskets.Commands.CreateBasketCommand;

public sealed class CreateBasketCommandValidator : AbstractValidator<CreateBasketCommand>
{
    public CreateBasketCommandValidator()
    {
        RuleFor(x => x.RequestId)
            .NotEmpty();

        RuleFor(x => x.CustomerId)
            .NotEmpty();

        RuleFor(x => x.Item.Id)
            .NotEmpty();

        RuleFor(x => x.Item.ProductName)
            .MaximumLength(DatabaseSchemaLength.DEFAULT_LENGTH);

        RuleFor(x => x.Item.Quantity)
            .GreaterThan(0);

        RuleFor(x => x.Item.Price)
            .GreaterThanOrEqualTo(0);
    }
}