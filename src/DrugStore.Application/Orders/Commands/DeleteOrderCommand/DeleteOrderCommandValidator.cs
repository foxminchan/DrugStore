using FluentValidation;

namespace DrugStore.Application.Orders.Commands.DeleteOrderCommand;

public sealed class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidator() => RuleFor(x => x.Id).NotEmpty();
}