using DrugStore.Application.Products.Commands.CreateProductCommand;
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
                string idempotencyKey,
                CreateProductPayload payload
            ) => await HandleAsync(new(idempotencyKey, payload)))
            .Produces<CreateProductResponse>()
            .WithTags(nameof(Product))
            .WithName("Create Product")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<CreateProductResponse> HandleAsync(
        CreateProductRequest request,
        CancellationToken cancellationToken = default)
    {
        if (!Guid.TryParse(request.Idempotency, out var requestId)) throw new InvalidIdempotencyException();

        var result = await sender.Send(
            new CreateProductCommand(
                requestId,
                request.Product.Name,
                request.Product.ProductCode,
                request.Product.Detail,
                request.Product.Quantity,
                request.Product.CategoryId,
                request.Product.ProductPrice
            ), cancellationToken);

        return new(result.Value);
    }
}