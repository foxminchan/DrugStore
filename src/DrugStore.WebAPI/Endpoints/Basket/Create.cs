using DrugStore.Application.Baskets.Commands.CreateBasketCommand;
using DrugStore.Infrastructure.Exception;
using DrugStore.WebAPI.Endpoints.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.WebAPI.Endpoints.Basket;

public sealed class Create(ISender sender) : IEndpoint<CreateBasketResponse, CreateBasketRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPost("/baskets", async (
                [FromHeader(Name = "X-Idempotency-Key")]
                string idempotencyKey,
                CreateBasketPayload payload
            ) => await HandleAsync(new(idempotencyKey, payload)))
            .Produces<CreateBasketResponse>()
            .WithTags(nameof(Basket))
            .WithName("Create Basket")
            .MapToApiVersion(new(1, 0))
            .RequireAuthorization();

    public async Task<CreateBasketResponse> HandleAsync(
        CreateBasketRequest request,
        CancellationToken cancellationToken = default)
    {
        if (!Guid.TryParse(request.Idempotency, out var requestId)) throw new InvalidIdempotencyException();

        var result = await sender.Send(
            new CreateBasketCommand(
                requestId,
                request.Basket.Id,
                request.Basket.Item
            ), cancellationToken);

        return new(result.Value);
    }
}