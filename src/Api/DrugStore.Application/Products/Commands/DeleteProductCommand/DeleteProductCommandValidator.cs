using FluentValidation;

namespace DrugStore.Application.Products.Commands.DeleteProductCommand;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
