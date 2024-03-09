using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.ProductAggregate.Primitives;
using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure.Storage.Local;
using DrugStore.Persistence;

namespace DrugStore.Application.Products.Commands.UpdateProductImageCommand;

public sealed class UpdateProductImageCommandHandler(
    Repository<Product> repository,
    ILocalStorage localStorage) : ICommandHandler<UpdateProductImageCommand, Result<ProductId>>
{
    public async Task<Result<ProductId>> Handle(UpdateProductImageCommand request, CancellationToken cancellationToken)
    {
        var product = await repository.GetByIdAsync(request.ProductId, cancellationToken);
        Guard.Against.NotFound(request.ProductId, product);

        var result = await localStorage.UploadFileAsync(request.Image, cancellationToken);

        product.Image = new(result, product.Name, nameof(Product));

        await repository.UpdateAsync(product, cancellationToken);

        return Result<ProductId>.Success(product.Id);
    }
}