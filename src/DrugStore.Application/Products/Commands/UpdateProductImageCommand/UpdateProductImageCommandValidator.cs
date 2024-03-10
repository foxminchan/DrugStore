using DrugStore.Application.Shared;
using DrugStore.Persistence.Helpers;
using FluentValidation;

namespace DrugStore.Application.Products.Commands.UpdateProductImageCommand;

public sealed class UpdateProductImageCommandValidator : AbstractValidator<UpdateProductImageCommand>
{
    public UpdateProductImageCommandValidator(FileValidator fileValidator)
    {
        RuleFor(x => x.ProductId).NotEmpty();

        RuleFor(x => x.Alt)
            .NotEmpty()
            .MaximumLength(DatabaseLengthHelper.DefaultLength);

        RuleFor(x => x.Image)
            .SetValidator(fileValidator);
    }
}