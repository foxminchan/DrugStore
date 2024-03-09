using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure.Storage.Local;
using DrugStore.Persistence;

namespace DrugStore.Application.Products.Commands.DeleteProductCommand;

public sealed class DeleteProductCommandHandler(
    Repository<Product> repository,
    ILocalStorage localStorage) : ICommandHandler<DeleteProductCommand, Result>
{
    public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await repository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, product);

        if (product.Image is { } && !string.IsNullOrWhiteSpace(product.Image.ImageUrl))
            await localStorage.RemoveFileAsync(product.Image.ImageUrl, cancellationToken);

        await repository.DeleteAsync(product, cancellationToken);
        product.DisableProduct(product.Id);
        return Result.Success();
    }
}