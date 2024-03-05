using FluentValidation;

namespace DrugStore.Application.Products.Commands.UpdateMainImageCommand;

public sealed class UpdateMainImageCommandValidator : AbstractValidator<UpdateMainImageCommand>
{
    public UpdateMainImageCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty();

        RuleFor(x => x.ImageUrl)
            .NotEmpty()
            .MaximumLength(100);
    }
}