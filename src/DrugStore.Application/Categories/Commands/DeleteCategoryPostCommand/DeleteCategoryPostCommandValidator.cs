using FluentValidation;

namespace DrugStore.Application.Categories.Commands.DeleteCategoryPostCommand;

public sealed class DeleteCategoryPostCommandValidator : AbstractValidator<DeleteCategoryPostCommand>
{
    public DeleteCategoryPostCommandValidator()
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty();

        RuleFor(x => x.PostId)
            .NotEmpty();
    }
}