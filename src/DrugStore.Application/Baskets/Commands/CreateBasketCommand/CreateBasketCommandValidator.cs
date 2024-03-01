using FluentValidation;

namespace DrugStore.Application.Baskets.Commands.CreateBasketCommand;

public sealed class CreateBasketCommandValidator : AbstractValidator<CreateBasketCommand>
{
    public CreateBasketCommandValidator()
    {
        RuleFor(x => x.RequestId)
            .NotEmpty();

        RuleFor(x => x.BasketRequest.CustomerId)
            .NotEmpty();

        RuleFor(x => x.BasketRequest.Item.Id)
            .NotEmpty();

        RuleFor(x => x.BasketRequest.Item.ProductName)
            .MaximumLength(100);

        RuleFor(x => x.BasketRequest.Item.Quantity)
            .GreaterThan(0);

        RuleFor(x => x.BasketRequest.Item.Price)
            .GreaterThan(0);
    }
}