using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Application.Abstractions.Commands;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.ProductAggregate.Specifications;
using DrugStore.Infrastructure.Storage.Local;
using DrugStore.Persistence.Repositories;

namespace DrugStore.Application.Products.Commands.DeleteProductCommand;

public sealed class DeleteProductCommandHandler(
    IRepository<Product> repository,
    ILocalStorage localStorage) : ICommandHandler<DeleteProductCommand, Result>
{
    public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var spec = new ProductByIdSpec(request.Id);
        var product = await repository.GetByIdAsync(spec, cancellationToken);
        Guard.Against.NotFound(request.Id, product);

        if (request.IsRemoveImage && product.Image is not null && !string.IsNullOrWhiteSpace(product.Image.ImageUrl))
            await localStorage.RemoveFileAsync(product.Image.ImageUrl, cancellationToken);

        product.SetDiscontinued();
        await repository.UpdateAsync(product, cancellationToken);

        product.DisableProduct(product.Id);
        return Result.Success();
    }
}