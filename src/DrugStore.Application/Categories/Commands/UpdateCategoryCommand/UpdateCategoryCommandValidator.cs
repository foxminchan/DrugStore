using DrugStore.Persistence.Helpers;
using FluentValidation;

namespace DrugStore.Application.Categories.Commands.UpdateCategoryCommand;

public sealed class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(DatabaseLengthHelper.DefaultLength);

        RuleFor(x => x.Description)
            .MaximumLength(DatabaseLengthHelper.LongLength);
    }
}