using DrugStore.Application.Orders.Commands.UpdateOrderCommand;
using DrugStore.WebAPI.Endpoints.Abstractions;
using DrugStore.WebAPI.Extensions;
using Mapster;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.Order;

public sealed class Update(ISender sender) : IEndpoint<UpdateOrderResponse, UpdateOrderRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPut("/orders", async (UpdateOrderRequest request) => await HandleAsync(request))
            .WithTags(nameof(Order))
            .WithName("Update Order")
            .Produces<UpdateOrderResponse>()
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<UpdateOrderResponse> HandleAsync(
        UpdateOrderRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new UpdateOrderCommand(
            request.Id,
            request.Code,
            request.CustomerId,
            request.Items
        ), cancellationToken);

        return new(result.Value.Adapt<OrderDetailDto>());
    }
}