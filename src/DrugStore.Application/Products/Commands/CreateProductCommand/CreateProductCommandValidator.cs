using DrugStore.Application.Categories.Validators;
using FluentValidation;

namespace DrugStore.Application.Products.Commands.CreateProductCommand;

public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator(CategoryIdValidator categoryIdValidator)
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.ProductCode)
            .MaximumLength(20);

        RuleFor(x => x.Detail)
            .MaximumLength(500);

        RuleFor(x => x.Quantity)
            .NotEmpty()
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.ProductPrice.Price)
            .NotEmpty()
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.ProductPrice.PriceSale)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(x => x.ProductPrice.Price);

        RuleFor(x => x.CategoryId)
            .SetValidator(categoryIdValidator);
    }
}