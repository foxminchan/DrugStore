using Ardalis.Result;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.ProductAggregate.Primitives;
using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure.Storage.Minio;
using DrugStore.Persistence;

namespace DrugStore.Application.Products.Commands.CreateProductCommand;

public sealed class CreateProductCommandHandler(
    Repository<Product> repository,
    IMinioService minioService) : IIdempotencyCommandHandler<CreateProductCommand, Result<ProductId>>
{
    public async Task<Result<ProductId>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        Product product = new(
            request.ProductRequest.Name,
            request.ProductRequest.ProductCode,
            request.ProductRequest.Detail,
            request.ProductRequest.Status,
            request.ProductRequest.Quantity,
            request.ProductRequest.CategoryId,
            request.ProductRequest.ProductPrice
        );

        await repository.AddAsync(product, cancellationToken);

        if (request.Images is null)
            return Result<ProductId>.Success(product.Id);

        foreach (var image in request.Images)
        {
            var result = await minioService.UploadFileAsync(image, nameof(Product));

            product.Images?.Add(new(
                result,
                request.ProductRequest.Name,
                nameof(Product),
                false
            ));
        }

        await repository.UpdateAsync(product, cancellationToken);

        return Result<ProductId>.Success(product.Id);
    }
}