using DrugStore.Application.Categories.Validators;
using DrugStore.Application.Products.Validators;
using DrugStore.Persistence.Helpers;
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
            .MaximumLength(DatabaseLengthHelper.DefaultLength);

        RuleFor(x => x.ProductRequest.ProductCode)
            .MaximumLength(DatabaseLengthHelper.SmallLength);

        RuleFor(x => x.ProductRequest.Detail)
            .MaximumLength(DatabaseLengthHelper.MaxLength);

        RuleFor(x => x.ProductRequest.Quantity)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.ProductRequest.ProductPrice)
            .SetValidator(productPriceValidator);

        RuleFor(x => x.ProductRequest.CategoryId)
            .SetValidator(categoryIdValidator);
    }
}