using FluentValidation;

namespace DrugStore.Application.Categories.Commands.CreateCategoryCommand;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Link)
            .MaximumLength(100);
    }
}
