using DrugStore.Application.Categories.Validators;
using DrugStore.Application.Products.Validators;
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
            .MaximumLength(50);

        RuleFor(x => x.ProductCode)
            .MaximumLength(20);

        RuleFor(x => x.Detail)
            .MaximumLength(500);

        RuleFor(x => x.Quantity)
            .NotEmpty()
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.ProductPrice)
            .SetValidator(productPriceValidator);

        RuleFor(x => x.CategoryId)
            .SetValidator(categoryIdValidator);
    }
}