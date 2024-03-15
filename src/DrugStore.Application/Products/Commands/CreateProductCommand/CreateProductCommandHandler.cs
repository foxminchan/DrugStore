﻿using Ardalis.Result;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.ProductAggregate.Primitives;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Products.Commands.CreateProductCommand;

public sealed class CreateProductCommandHandler(IRepository<Product> repository)
    : IIdempotencyCommandHandler<CreateProductCommand, Result<ProductId>>
{
    public async Task<Result<ProductId>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        Product product = new(
            request.Name,
            request.ProductCode,
            request.Detail,
            request.Quantity,
            request.CategoryId,
            request.ProductPrice
        );

        await repository.AddAsync(product, cancellationToken);

        return Result<ProductId>.Success(product.Id);
    }
}