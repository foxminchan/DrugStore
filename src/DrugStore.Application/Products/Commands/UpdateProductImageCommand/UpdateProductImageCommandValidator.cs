using DrugStore.Application.Shared;
using FluentValidation;

namespace DrugStore.Application.Products.Commands.UpdateProductImageCommand;

public sealed class UpdateProductImageCommandValidator : AbstractValidator<UpdateProductImageCommand>
{
    public UpdateProductImageCommandValidator(FileValidator fileValidator)
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.Images)
            .ForEach(x => x.SetValidator(fileValidator));
    }
}