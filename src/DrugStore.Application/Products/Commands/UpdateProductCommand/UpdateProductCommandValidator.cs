using DrugStore.Application.Categories.Validators;
using DrugStore.Application.Products.Validators;
using DrugStore.Persistence.Helpers;
using FluentValidation;

namespace DrugStore.Application.Products.Commands.UpdateProductCommand;

public sealed class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator(
        ProductPriceValidator productPriceValidator, 
        CategoryIdValidator categoryIdValidator)
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(DatabaseLengthHelper.DefaultLength);

        RuleFor(x => x.ProductCode)
            .MaximumLength(DatabaseLengthHelper.SmallLength);

        RuleFor(x => x.Detail)
            .MaximumLength(DatabaseLengthHelper.MaxLength);

        RuleFor(x => x.Quantity)
            .NotEmpty()
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.ProductPrice)
            .SetValidator(productPriceValidator);

        RuleFor(x => x.CategoryId)
            .SetValidator(categoryIdValidator);
    }
}