using DrugStore.Application.Categories.Validators;
using DrugStore.Application.Products.Validators;
using DrugStore.Application.Shared;
using FluentValidation;

namespace DrugStore.Application.Products.Commands.CreateProductCommand;

public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator(
        CategoryIdValidator categoryIdValidator,
        ProductPriceValidator productPriceValidator)
    {
        RuleFor(x => x.ProductRequest.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.ProductRequest.ProductCode)
            .MaximumLength(20);

        RuleFor(x => x.ProductRequest.Detail)
            .MaximumLength(500);

        RuleFor(x => x.ProductRequest.Quantity)
            .NotEmpty()
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.ProductRequest.ProductPrice)
            .SetValidator(productPriceValidator);

        RuleFor(x => x.ProductRequest.CategoryId)
            .SetValidator(categoryIdValidator);
    }
}