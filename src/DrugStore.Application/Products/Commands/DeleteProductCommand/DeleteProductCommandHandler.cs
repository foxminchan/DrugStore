using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.SharedKernel;
using DrugStore.Persistence;

namespace DrugStore.Application.Products.Commands.DeleteProductCommand;

public sealed class DeleteProductCommandHandler(Repository<Product> repository)
    : ICommandHandler<DeleteProductCommand, Result>
{
    public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await repository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, product);
        product.DisableProduct(product.Id);
        await repository.DeleteAsync(product, cancellationToken);
        return Result.Success();
    }
}