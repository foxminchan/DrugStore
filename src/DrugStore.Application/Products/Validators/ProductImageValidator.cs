using DrugStore.Domain.ProductAggregate.ValueObjects;
using DrugStore.Persistence.Constants;
using FluentValidation;

namespace DrugStore.Application.Products.Validators;

public sealed class ProductImageValidator : AbstractValidator<ProductImage>
{
    public ProductImageValidator()
    {
        RuleFor(x => x.ImageUrl)
            .NotEmpty()
            .MaximumLength(DatabaseSchemaLength.DEFAULT_LENGTH);

        RuleFor(x => x.Alt)
            .MaximumLength(DatabaseSchemaLength.DEFAULT_LENGTH);

        RuleFor(x => x.Title)
            .MaximumLength(DatabaseSchemaLength.DEFAULT_LENGTH);
    }
}