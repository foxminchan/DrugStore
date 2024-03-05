using DrugStore.Domain.ProductAggregate.ValueObjects;
using FluentValidation;

namespace DrugStore.Application.Products.Validators;

public sealed class ProductImageValidator : AbstractValidator<ProductImage>
{
    public ProductImageValidator()
    {
        RuleFor(x => x.ImageUrl)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Alt)
            .MaximumLength(100);

        RuleFor(x => x.Title)
            .MaximumLength(100);
    }
}