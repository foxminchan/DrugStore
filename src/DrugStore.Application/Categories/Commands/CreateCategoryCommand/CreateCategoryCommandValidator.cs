using DrugStore.Persistence.Helpers;
using FluentValidation;

namespace DrugStore.Application.Categories.Commands.CreateCategoryCommand;

public sealed class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.RequestId)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(DatabaseLengthHelper.DefaultLength);

        RuleFor(x => x.Description)
            .MaximumLength(DatabaseLengthHelper.LongLength);
    }
}