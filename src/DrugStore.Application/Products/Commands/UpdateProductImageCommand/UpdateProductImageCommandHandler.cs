using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.ProductAggregate.Primitives;
using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure.Storage.Local;

namespace DrugStore.Application.Products.Commands.UpdateProductImageCommand;

public sealed class UpdateProductImageCommandHandler(
    IRepository<Product> repository,
    ILocalStorage localStorage) : ICommandHandler<UpdateProductImageCommand, Result<ProductId>>
{
    public async Task<Result<ProductId>> Handle(UpdateProductImageCommand request, CancellationToken cancellationToken)
    {
        var product = await repository.GetByIdAsync(request.ProductId, cancellationToken);
        Guard.Against.NotFound(request.ProductId, product);

        if (product.Image is not null && !string.IsNullOrWhiteSpace(product.Image.ImageUrl))
            await localStorage.RemoveFileAsync(product.Image.ImageUrl, cancellationToken);

        var result = await localStorage.UploadFileAsync(request.Image, cancellationToken);

        product.Image = new(result, request.Alt, product.Name);

        await repository.UpdateAsync(product, cancellationToken);

        return Result<ProductId>.Success(product.Id);
    }
}