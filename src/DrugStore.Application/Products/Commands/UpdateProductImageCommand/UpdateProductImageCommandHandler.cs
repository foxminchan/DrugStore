using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.ProductAggregate.Primitives;
using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure.Storage.Minio;
using DrugStore.Persistence;

namespace DrugStore.Application.Products.Commands.UpdateProductImageCommand;

public sealed class UpdateProductImageCommandHandler(
    Repository<Product> repository,
    IMinioService minioService) : ICommandHandler<UpdateProductImageCommand, Result<ProductId>>
{
    public async Task<Result<ProductId>> Handle(UpdateProductImageCommand request, CancellationToken cancellationToken)
    {
        var product = await repository.GetByIdAsync(request.ProductId, cancellationToken);
        Guard.Against.NotFound(request.ProductId, product);

        foreach (var image in request.Images)
        {
            var result = await minioService.UploadFileAsync(image, nameof(Product).ToLowerInvariant());

            product.Images?.Add(new(
                result,
                product.Name,
                nameof(Product),
                false
            ));
        }

        await repository.UpdateAsync(product, cancellationToken);

        return Result<ProductId>.Success(product.Id);
    }
}