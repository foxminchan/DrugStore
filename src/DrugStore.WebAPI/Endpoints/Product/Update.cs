﻿using DrugStore.Application.Products.Commands.UpdateProductCommand;
using DrugStore.Domain.CategoryAggregate.Primitives;
using DrugStore.Domain.ProductAggregate.Primitives;
using DrugStore.WebAPI.Endpoints.Abstractions;
using DrugStore.WebAPI.Extensions;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.WebAPI.Endpoints.Product;

public sealed class Update(ISender sender) : IEndpoint<UpdateProductResponse, UpdateProductRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPut("/products", async (
                    [FromForm] ProductId id,
                    [FromForm] string name,
                    [FromForm] string? productCode,
                    [FromForm] string? detail,
                    [FromForm] int quantity,
                    [FromForm] CategoryId? categoryId,
                    [FromForm] decimal price,
                    [FromForm] decimal priceSale,
                    [FromForm] bool isDeleteImage,
                    [FromForm] IFormFile? image,
                    [FromForm] string? alt)
                => await HandleAsync(new(
                    id, name, productCode, detail, quantity, categoryId, price, priceSale, isDeleteImage, image, alt)
                ))
            .Produces<UpdateProductResponse>()
            .WithTags(nameof(Product))
            .WithName("Update Product")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<UpdateProductResponse> HandleAsync(
        UpdateProductRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(
            new UpdateProductCommand(
                request.Id,
                request.Name,
                request.ProductCode,
                request.Detail,
                request.Quantity,
                request.CategoryId,
                new(request.Price, request.PriceSale),
                request.IsDeleteImage,
                request.Image,
                request.Alt
            ), cancellationToken);
        return new(result.Value.Adapt<ProductDto>());
    }
}