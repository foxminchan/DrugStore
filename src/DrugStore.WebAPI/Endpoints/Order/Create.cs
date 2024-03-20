using DrugStore.Application.Orders.Commands.CreateOrderCommand;
using DrugStore.Infrastructure.Exception;
using DrugStore.WebAPI.Endpoints.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DrugStore.WebAPI.Endpoints.Order;

public sealed class Create(ISender sender) : IEndpoint<CreateOrderResponse, CreateOrderRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPost("/orders", async (
                [FromHeader(Name = "X-Idempotency-Key")]
                string idempotencyKey,
                CreateOrderPayload payload
            ) => await HandleAsync(new(idempotencyKey, payload)))
            .Produces<CreateOrderResponse>()
            .WithTags(nameof(Order))
            .WithName("Create Order")
            .MapToApiVersion(new(1, 0))
            .RequireAuthorization();

    public async Task<CreateOrderResponse> HandleAsync(
        CreateOrderRequest request,
        CancellationToken cancellationToken = default)
    {
        if (!Guid.TryParse(request.Idempotency, out var requestId)) throw new InvalidIdempotencyException();

        var result = await sender.Send(
            new CreateOrderCommand(
                requestId,
                request.Order.Code,
                request.Order.CustomerId,
                request.Order.Items
            ), cancellationToken);

        return new(result.Value);
    }
}