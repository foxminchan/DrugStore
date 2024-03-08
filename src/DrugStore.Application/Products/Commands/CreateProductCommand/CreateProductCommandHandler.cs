using Ardalis.Result;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.ProductAggregate.Primitives;
using DrugStore.Domain.SharedKernel;
using DrugStore.Persistence;

namespace DrugStore.Application.Products.Commands.CreateProductCommand;

public sealed class CreateProductCommandHandler(Repository<Product> repository) 
    : IIdempotencyCommandHandler<CreateProductCommand, Result<ProductId>>
{
    public async Task<Result<ProductId>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        Product product = new(
            request.ProductRequest.Name,
            request.ProductRequest.ProductCode,
            request.ProductRequest.Detail,
            request.ProductRequest.Quantity,
            request.ProductRequest.CategoryId,
            request.ProductRequest.ProductPrice
        );

        await repository.AddAsync(product, cancellationToken);

        return Result<ProductId>.Success(product.Id);
    }
}