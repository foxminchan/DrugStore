using DrugStore.Application.Categories.Validators;
using DrugStore.Application.Products.Validators;
using DrugStore.Persistence.Constants;
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
            .MaximumLength(DatabaseSchemaLength.DefaultLength);

        RuleFor(x => x.ProductCode)
            .MaximumLength(DatabaseSchemaLength.SmallLength);

        RuleFor(x => x.Detail)
            .MaximumLength(DatabaseSchemaLength.MaxLength);

        RuleFor(x => x.Quantity)
            .NotEmpty()
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.ProductPrice)
            .SetValidator(productPriceValidator);

        RuleFor(x => x.CategoryId)
            .SetValidator(categoryIdValidator);
    }
}