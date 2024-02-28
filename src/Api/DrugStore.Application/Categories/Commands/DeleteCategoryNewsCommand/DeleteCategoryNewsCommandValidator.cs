using FluentValidation;

namespace DrugStore.Application.Categories.Commands.DeleteCategoryNewsCommand;

public sealed class DeleteCategoryNewsCommandValidator : AbstractValidator<DeleteCategoryNewsCommand>
{
    public DeleteCategoryNewsCommandValidator()
    {
        RuleFor(x => x.CategoryId)
            .NotEmpty();

        RuleFor(x => x.NewsId)
            .NotEmpty();
    }
}
