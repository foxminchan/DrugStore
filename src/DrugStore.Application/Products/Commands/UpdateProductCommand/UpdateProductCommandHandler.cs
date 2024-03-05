using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Application.Products.ViewModels;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.ProductAggregate.Enums;
using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure.Storage.Cloudinary;
using DrugStore.Persistence;
using Mapster;

namespace DrugStore.Application.Products.Commands.UpdateProductCommand;

public sealed class UpdateProductCommandHandler(
    Repository<Product> repository,
    ICloudinaryService cloudinaryService) : ICommandHandler<UpdateProductCommand, Result<ProductVm>>
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

        if (request.ProductRequest.ImageUrls is { })
        {
            var tasks = request.ProductRequest.ImageUrls.Select(cloudinaryService.DeletePhotoAsync);
            await Task.WhenAll(tasks);
        }

        if (request.Images is { })
            foreach (var image in request.Images)
            {
                var result = await cloudinaryService.AddPhotoAsync(image);
                product.Images?.Add(new(
                    result.Value.Url,
                    request.ProductRequest.Name,
                    result.Value.PublishId,
                    false
                ));
            }

        await repository.UpdateAsync(product, cancellationToken);

        if (product.Status == ProductStatus.OutOfStock || product.Status == ProductStatus.Discontinued)
            product.DisableProduct(product.Id);

        return Result<ProductVm>.Success(product.Adapt<ProductVm>());
    }
}