﻿using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Application.Products.ViewModel;
using DrugStore.Domain.Product;
using DrugStore.Domain.SharedKernel;
using DrugStore.Persistence;
using Mapster;

namespace DrugStore.Application.Products.Commands.UpdateProductCommand;

public sealed class UpdateProductCommandHandler(Repository<Product> repository)
    : ICommandHandler<UpdateProductCommand, Result<ProductVm>>
{
    public async Task<Result<ProductVm>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await repository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, product);
        product.Update(
            request.Title, 
            request.ProductCode, 
            request.Detail, 
            request.Status, 
            request.Quantity, 
            request.CategoryId,
            request.OriginalPrice, 
            request.Price, 
            request.PriceSale);
        await repository.UpdateAsync(product, cancellationToken);
        return Result<ProductVm>.Success(product.Adapt<ProductVm>());
    }
}