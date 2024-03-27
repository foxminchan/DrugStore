using DrugStore.Application.Products.Validators;
using DrugStore.Application.Shared;
using DrugStore.Persistence.Constants;
using FluentValidation;

namespace DrugStore.Application.Products.Commands.CreateProductCommand;

public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator(ProductPriceValidator productPriceValidator, FileValidator fileValidator)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(DatabaseSchemaLength.DEFAULT_LENGTH);

        RuleFor(x => x.ProductCode)
            .MaximumLength(DatabaseSchemaLength.SMALL_LENGTH);

        RuleFor(x => x.Detail)
            .MaximumLength(DatabaseSchemaLength.MAX_LENGTH);

        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.ProductPrice)
            .SetValidator(productPriceValidator);

        RuleFor(x => x.Alt)
            .MaximumLength(DatabaseSchemaLength.DEFAULT_LENGTH);

        RuleFor(x => x.Image)
            .SetValidator(fileValidator!);
    }
}