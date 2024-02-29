using Ardalis.Result;

using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.SharedKernel;
using DrugStore.Persistence;

namespace DrugStore.Application.Products.Commands.CreateProductCommand;

public sealed class CreateProductCommandHandler(Repository<Product> repository)
    : ICommandHandler<CreateProductCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product(
            request.Title,
            request.ProductCode,
            request.Detail,
            request.Status,
            request.Quantity,
            request.CategoryId,
            request.ProductPrice);

        await repository.AddAsync(product, cancellationToken);

        return Result<Guid>.Success(product.Id);
    }
}
