using DrugStore.Application.Categories.Validators;
using DrugStore.Application.Products.Validators;
using DrugStore.Application.Shared;
using FluentValidation;

namespace DrugStore.Application.Products.Commands.UpdateProductCommand;

public sealed class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator(
        ProductPriceValidator productPriceValidator,
        FileValidator fileValidator,
        CategoryIdValidator categoryIdValidator)
    {
        RuleFor(x => x.ProductRequest.Id)
            .NotEmpty();

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

        RuleFor(x => x.Images)
            .ForEach(x => x.SetValidator(fileValidator));

        RuleFor(x => x.ProductRequest.CategoryId)
            .SetValidator(categoryIdValidator);
    }
}