using DrugStore.Domain.ProductAggregate.ValueObjects;
using FluentValidation;

namespace DrugStore.Application.Products.Validators;

public sealed class ProductPriceValidator : AbstractValidator<ProductPrice>
{
    public ProductPriceValidator()
    {
        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.PriceSale)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(x => x.Price);
    }
}