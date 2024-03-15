using DrugStore.Application.Products.Validators;
using DrugStore.Persistence.Constants;
using FluentValidation;

namespace DrugStore.Application.Products.Commands.CreateProductCommand;

public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator(ProductPriceValidator productPriceValidator)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(DatabaseSchemaLength.DefaultLength);

        RuleFor(x => x.ProductCode)
            .MaximumLength(DatabaseSchemaLength.SmallLength);

        RuleFor(x => x.Detail)
            .MaximumLength(DatabaseSchemaLength.MaxLength);

        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.ProductPrice)
            .SetValidator(productPriceValidator);
    }
}