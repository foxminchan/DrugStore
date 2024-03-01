using FluentValidation;

namespace DrugStore.Application.Categories.Commands.CreateCategoryCommand;

public sealed class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.RequestId)
            .NotEmpty();

        RuleFor(x => x.CategoryRequest.Title)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.CategoryRequest.Link)
            .MaximumLength(100);
    }
}