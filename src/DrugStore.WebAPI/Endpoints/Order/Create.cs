using DrugStore.Application.Orders.Commands.CreateOrderCommand;
using DrugStore.Infrastructure.Endpoints;
using DrugStore.Infrastructure.Exception;
using DrugStore.Infrastructure.RateLimiter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.WebAPI.Endpoints.Order;

public sealed class Create(ISender sender) : IEndpoint<IResult, CreateOrderRequest>
{
    public async Task<IResult> HandleAsync(
        CreateOrderRequest request,
        CancellationToken cancellationToken = default)
    {
        if (!Guid.TryParse(request.Idempotency, out var requestId)) throw new InvalidIdempotencyException();

        CreateOrderCommand command = new(requestId, request.Order.Code, request.Order.CustomerId, request.Order.Items);

        var result = await sender.Send(command, cancellationToken);

        CreateOrderResponse response = new(result.Value);

        return Results.Created($"/api/v1/orders/{response.Id}", response);
    }

    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPost("/orders", async (
                [FromHeader(Name = "X-Idempotency-Key")]
                string idempotencyKey,
                CreateOrderPayload payload
            ) => await HandleAsync(new(idempotencyKey, payload)))
            .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
            .WithTags(nameof(Order))
            .WithName("Create Order")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();
}