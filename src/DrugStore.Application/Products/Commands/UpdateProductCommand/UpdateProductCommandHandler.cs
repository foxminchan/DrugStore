using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Application.Products.ViewModels;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.ProductAggregate.Enums;
using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure.Storage.Minio;
using DrugStore.Persistence;
using Mapster;

namespace DrugStore.Application.Products.Commands.UpdateProductCommand;

public sealed class UpdateProductCommandHandler(
    Repository<Product> repository,
    IMinioService minioService) : ICommandHandler<UpdateProductCommand, Result<ProductVm>>
{
    public async Task<Result<ProductVm>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await repository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, product);

        product.Update(
            request.Name,
            request.ProductCode,
            request.Detail,
            request.Status,
            request.Quantity,
            request.CategoryId,
            request.ProductPrice
        );

        await RemoveObsoleteImagesAsync(product, request.ImageUrls);
        await repository.UpdateAsync(product, cancellationToken);

        if (product.Status == ProductStatus.OutOfStock || product.Status == ProductStatus.Discontinued)
            product.DisableProduct(product.Id);

        return Result<ProductVm>.Success(product.Adapt<ProductVm>());
    }

    private async Task RemoveObsoleteImagesAsync(Product product, IEnumerable<string>? imageUrls)
    {
        if (imageUrls is null || product.Images is null)
            return;

        var tasks = imageUrls.Select(async imageUrl =>
        {
            var image = product.Images.FirstOrDefault(i => i.ImageUrl == imageUrl);
            if (image is { })
            {
                if (image.ImageUrl is { })
                    await minioService.RemoveFileAsync(nameof(Product), image.ImageUrl);
                product.Images?.Remove(image);
            }
        });

        await Task.WhenAll(tasks).ConfigureAwait(false);
    }
}