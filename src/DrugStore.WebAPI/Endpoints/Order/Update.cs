using DrugStore.Application.Orders.Commands.UpdateOrderCommand;
using DrugStore.Infrastructure.Endpoints;
using DrugStore.Infrastructure.RateLimiter;
using Mapster;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.Order;

public sealed class Update(ISender sender) : IEndpoint<IResult, UpdateOrderRequest>
{
    public async Task<IResult> HandleAsync(
        UpdateOrderRequest request,
        CancellationToken cancellationToken = default)
    {
        UpdateOrderCommand command = new(request.Id, request.Code, request.CustomerId, request.Items);

        var result = await sender.Send(command, cancellationToken);

        var response = new UpdateOrderResponse
        {
            Order = result.Value.Adapt<OrderDetailDto>()
        };

        return Results.Ok(response);
    }

    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPut("/orders", async (UpdateOrderRequest request) => await HandleAsync(request))
            .Produces<UpdateOrderResponse>()
            .WithTags(nameof(Order))
            .WithName("Update Order")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();
}