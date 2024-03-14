using DrugStore.Application.Orders.Commands.DeleteOrderCommand;
using DrugStore.Domain.OrderAggregate.Primitives;
using DrugStore.Domain.SharedKernel;
using DrugStore.WebAPI.Extensions;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.Order;

public sealed class Delete(ISender sender) : IEndpoint<Unit, DeleteOrderRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapDelete("orders/{id}", async (OrderId id) => await HandleAsync(new(id)))
            .WithTags(nameof(Order))
            .WithName("Delete Order")
            .Produces<Unit>()
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<Unit> HandleAsync(
        DeleteOrderRequest request,
        CancellationToken cancellationToken = default)
    {
        await sender.Send(new DeleteOrderCommand(request.Id), cancellationToken);
        return Unit.Value;
    }
}