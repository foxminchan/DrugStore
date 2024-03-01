using FluentValidation;

namespace DrugStore.Application.Categories.Commands.DeleteCategoryCommand;

public sealed class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidator() => RuleFor(x => x.Id).NotEmpty();
}