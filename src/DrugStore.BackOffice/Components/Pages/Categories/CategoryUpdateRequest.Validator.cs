using FluentValidation;

namespace DrugStore.BackOffice.Components.Pages.Categories;

public sealed class CategoryUpdateRequestValidator : AbstractValidator<CategoryUpdateRequest>
{
    public CategoryUpdateRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required");
    }
}