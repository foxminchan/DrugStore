using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Application.Products.ViewModels;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.ProductAggregate.Enums;
using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure.Storage.Minio;
using DrugStore.Persistence;
using Mapster;
using Microsoft.AspNetCore.Http;

namespace DrugStore.Application.Products.Commands.UpdateProductCommand;

public sealed class UpdateProductCommandHandler(
    Repository<Product> repository,
    IMinioService minioService) : ICommandHandler<UpdateProductCommand, Result<ProductVm>>
{
    public async Task<Result<ProductVm>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await repository.GetByIdAsync(request.ProductRequest.Id, cancellationToken);
        Guard.Against.NotFound(request.ProductRequest.Id, product);

        product.Update(
            request.ProductRequest.Name,
            request.ProductRequest.ProductCode,
            request.ProductRequest.Detail,
            request.ProductRequest.Status,
            request.ProductRequest.Quantity,
            request.ProductRequest.CategoryId,
            request.ProductRequest.ProductPrice
        );

        await RemoveObsoleteImagesAsync(product, request.ProductRequest.ImageUrls);
        await UploadNewImagesAsync(product, request.Images);
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

    private async Task UploadNewImagesAsync(Product product, IEnumerable<IFormFile>? images)
    {
        if (images is null)
            return;

        var uploadTasks = images.Select(async image =>
        {
            var result = await minioService.UploadFileAsync(image, nameof(Product));
            product.Images?.Add(new(
                result,
                product.Name,
                nameof(Product),
                false
            ));
        });

        await Task.WhenAll(uploadTasks).ConfigureAwait(false);
    }
}