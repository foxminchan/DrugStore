using DrugStore.Application.Baskets.Commands.CreateBasketCommand;
using DrugStore.Infrastructure.Exception;
using DrugStore.WebAPI.Endpoints.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.WebAPI.Endpoints.Basket;

public sealed class Create(ISender sender) : IEndpoint<IResult, CreateBasketRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPost("/baskets", async (
                [FromHeader(Name = "X-Idempotency-Key")]
                string idempotencyKey,
                CreateBasketPayload payload
            ) => await HandleAsync(new(idempotencyKey, payload)))
            .Produces<CreateBasketResponse>(StatusCodes.Status201Created)
            .WithTags(nameof(Basket))
            .WithName("Create Basket")
            .MapToApiVersion(new(1, 0))
            .RequireAuthorization();

    public async Task<IResult> HandleAsync(
        CreateBasketRequest request,
        CancellationToken cancellationToken = default)
    {
        if (!Guid.TryParse(request.Idempotency, out var requestId)) throw new InvalidIdempotencyException();

        CreateBasketCommand command = new(requestId, request.Basket.Id, request.Basket.Item);

        var result = await sender.Send(command, cancellationToken);

        CreateBasketResponse response = new(result.Value);

        return Results.Created($"/api/v1/baskets/{response.Id}", response);
    }
}