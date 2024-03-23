using DrugStore.Application.Products.Commands.CreateProductCommand;
using DrugStore.Domain.CategoryAggregate.Primitives;
using DrugStore.Infrastructure.Exception;
using DrugStore.WebAPI.Endpoints.Abstractions;
using DrugStore.WebAPI.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.WebAPI.Endpoints.Product;

public sealed class Create(ISender sender) : IEndpoint<CreateProductResponse, CreateProductRequest>
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
            .Produces<CreateProductResponse>()
            .WithTags(nameof(Product))
            .WithName("Create Product")
            .MapToApiVersion(new(1, 0))
            .DisableAntiforgery()
            .RequirePerUserRateLimit();

    public async Task<CreateProductResponse> HandleAsync(
        CreateProductRequest request,
        CancellationToken cancellationToken = default)
    {
        if (!Guid.TryParse(request.Idempotency, out var requestId)) throw new InvalidIdempotencyException();

        var result = await sender.Send(
            new CreateProductCommand(
                requestId,
                request.ProductName,
                request.ProductCode,
                request.Detail,
                request.Quantity,
                request.CategoryId,
                new(request.Price, request.PriceSale),
                request.Image,
                request.Alt
            ), cancellationToken);

        return new(result.Value);
    }
}