using FluentValidation;

namespace DrugStore.Application.Baskets.Commands.DeleteBasketCommand;

public sealed class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(x => x.BasketId).NotEmpty();
    }
}
