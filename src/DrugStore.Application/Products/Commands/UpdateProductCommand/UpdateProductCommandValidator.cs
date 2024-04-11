using DrugStore.Application.Abstractions.Validators;
using DrugStore.Application.Products.Validators;
using DrugStore.Persistence.Constants;
using FluentValidation;

namespace DrugStore.Application.Products.Commands.UpdateProductCommand;

public sealed class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator(ProductPriceValidator productPriceValidator, FileValidator fileValidator)
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(DatabaseSchemaLength.DEFAULT_LENGTH);

        RuleFor(x => x.ProductCode)
            .MaximumLength(DatabaseSchemaLength.SMALL_LENGTH);

        RuleFor(x => x.Detail)
            .MaximumLength(DatabaseSchemaLength.MAX_LENGTH);

        RuleFor(x => x.Quantity)
            .NotEmpty()
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.ProductPrice)
            .SetValidator(productPriceValidator);

        RuleFor(x => x.Alt)
            .MaximumLength(DatabaseSchemaLength.DEFAULT_LENGTH);

        RuleFor(x => x.Image)
            .SetValidator(fileValidator!);
    }
}