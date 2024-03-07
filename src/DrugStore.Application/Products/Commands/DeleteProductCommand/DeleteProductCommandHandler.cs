using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure.Storage.Minio;
using DrugStore.Persistence;

namespace DrugStore.Application.Products.Commands.DeleteProductCommand;

public sealed class DeleteProductCommandHandler(
    Repository<Product> repository,
    IMinioService minioService) : ICommandHandler<DeleteProductCommand, Result>
{
    public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await repository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, product);

        if (product.Images is { })
        {
            var tasks = product.Images.Select(image => image.ImageUrl is { }
                ? Task.FromResult(minioService.RemoveFileAsync(nameof(Product), image.ImageUrl))
                : Task.CompletedTask
            );
            await Task.WhenAll(tasks).ConfigureAwait(false);
        }

        await repository.DeleteAsync(product, cancellationToken);
        product.DisableProduct(product.Id);
        return Result.Success();
    }
}