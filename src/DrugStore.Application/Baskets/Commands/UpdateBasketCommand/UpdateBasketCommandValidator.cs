using DrugStore.Persistence.Constants;
using FluentValidation;

namespace DrugStore.Application.Baskets.Commands.UpdateBasketCommand;

public sealed class UpdateBasketCommandValidator : AbstractValidator<UpdateBasketCommand>
{
    public UpdateBasketCommandValidator()
    {
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