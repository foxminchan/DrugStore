using FluentValidation;

namespace DrugStore.Application.Products.Commands.UpdateProductCommand;

public sealed class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

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

        RuleFor(x => x.OriginalPrice)
            .NotEmpty()
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Price)
            .NotEmpty()
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.PriceSale)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(x => x.Price);
    }
}
