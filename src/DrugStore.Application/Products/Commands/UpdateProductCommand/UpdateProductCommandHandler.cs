﻿using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Application.Products.ViewModels;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.ProductAggregate.Specifications;
using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure.Storage.Local;
using MapsterMapper;

namespace DrugStore.Application.Products.Commands.UpdateProductCommand;

public sealed class UpdateProductCommandHandler(
    IMapper mapper,
    IRepository<Product> repository,
    ILocalStorage localStorage) : ICommandHandler<UpdateProductCommand, Result<ProductVm>>
{
    public async Task<Result<ProductVm>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await repository.GetByIdAsync(new ProductByIdSpec(request.Id), cancellationToken);
        Guard.Against.NotFound(request.Id, product);

        if (request.IsDeleteImage || request.Image is not null)
            await RemoveObsoleteImagesAsync(product);

        var result = string.Empty;

        if (request.Image is not null)
            result = await localStorage.UploadFileAsync(request.Image, cancellationToken);

        product.Update(
            request.Name,
            request.ProductCode,
            request.Detail,
            request.Quantity,
            request.CategoryId,
            request.ProductPrice,
            string.IsNullOrWhiteSpace(result) ? null : new(result, request.Alt ?? request.Name, request.Name)
        );

        await repository.UpdateAsync(product, cancellationToken);
        return Result<ProductVm>.Success(mapper.Map<ProductVm>(product));
    }

    private async Task RemoveObsoleteImagesAsync(Product product)
    {
        if (product.Image is not null && !string.IsNullOrWhiteSpace(product.Image.ImageUrl))
            await localStorage.RemoveFileAsync(product.Image.ImageUrl);
    }
}