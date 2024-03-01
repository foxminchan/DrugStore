using FluentValidation;

namespace DrugStore.Application.Products.Commands.DeleteProductCommand;

public sealed class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator() => RuleFor(x => x.Id).NotEmpty();
}