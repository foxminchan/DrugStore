﻿using DrugStore.Application.Products.Commands.CreateProductCommand;
using DrugStore.Domain.CategoryAggregate.Primitives;
using DrugStore.Infrastructure.Exception;
using DrugStore.WebAPI.Endpoints.Abstractions;
using DrugStore.WebAPI.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.WebAPI.Endpoints.Product;

public sealed class Create(ISender sender) : IEndpoint<IResult, CreateProductRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPost("/products", async (
                [FromHeader(Name = "X-Idempotency-Key")]
                string idempotency,
                [FromForm] string name,
                [FromForm] string? productCode,
                [FromForm] string? detail,
                [FromForm] int quantity,
                [FromForm] CategoryId? categoryId,
                [FromForm] decimal price,
                [FromForm] decimal priceSale,
                [FromForm] IFormFile? image,
                [FromForm] string? alt
            ) => await HandleAsync(new(
                idempotency, name, productCode, detail, quantity, categoryId, price, priceSale, image, alt)
            ))
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .WithTags(nameof(Product))
            .WithName("Create Product")
            .MapToApiVersion(new(1, 0))
            .DisableAntiforgery()
            .RequirePerUserRateLimit();

    public async Task<IResult> HandleAsync(
        CreateProductRequest request,
        CancellationToken cancellationToken = default)
    {
        if (!Guid.TryParse(request.Idempotency, out var requestId)) throw new InvalidIdempotencyException();

        CreateProductCommand command = new(
            requestId,
            request.ProductName,
            request.ProductCode,
            request.Detail,
            request.Quantity,
            request.CategoryId,
            new(request.Price, request.PriceSale),
            request.Image,
            request.Alt
        );

        var result = await sender.Send(command, cancellationToken);

        CreateProductResponse response = new(result.Value);

        return Results.Created($"/api/v1/products/{response.Id}", response);
    }
}