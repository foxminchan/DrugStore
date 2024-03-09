using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Application.Products.ViewModels;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure.Storage.Local;
using DrugStore.Persistence;
using Mapster;

namespace DrugStore.Application.Products.Commands.UpdateProductCommand;

public sealed class UpdateProductCommandHandler(
    Repository<Product> repository,
    ILocalStorage localStorage) : ICommandHandler<UpdateProductCommand, Result<ProductVm>>
{
    public async Task<Result<ProductVm>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await repository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, product);

        product.Update(
            request.Name,
            request.ProductCode,
            request.Detail,
            request.Quantity,
            request.CategoryId,
            request.ProductPrice
        );

        await RemoveObsoleteImagesAsync(product, request.ImageUrl);
        await repository.UpdateAsync(product, cancellationToken);

        return Result<ProductVm>.Success(product.Adapt<ProductVm>() with { Category = product.Category?.Name });
    }

    private async Task RemoveObsoleteImagesAsync(Product product, string? imageUrl)
    {
        if (imageUrl is null || product.Image is null)
            return;

        if (!string.IsNullOrWhiteSpace(product.Image.ImageUrl))
            await localStorage.RemoveFileAsync(product.Image.ImageUrl);
    }
}