using FluentValidation;

namespace DrugStore.Application.Categories.Commands.UpdateCategoryCommand;

public sealed class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Link)
            .MaximumLength(100);
    }
}