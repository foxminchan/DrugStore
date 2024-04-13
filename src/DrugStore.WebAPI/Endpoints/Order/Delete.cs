using DrugStore.Application.Orders.Commands.DeleteOrderCommand;
using DrugStore.Domain.OrderAggregate.Primitives;
using DrugStore.WebAPI.Endpoints.Abstractions;
using DrugStore.WebAPI.Extensions;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.Order;

public sealed class Delete(ISender sender) : IEndpoint<IResult, DeleteOrderRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapDelete("orders/{id}", async (OrderId id) => await HandleAsync(new(id)))
            .Produces(StatusCodes.Status204NoContent)
            .WithTags(nameof(Order))
            .WithName("Delete Order")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<IResult> HandleAsync(
        DeleteOrderRequest request,
        CancellationToken cancellationToken = default)
    {
        DeleteOrderCommand command = new(request.Id);

        await sender.Send(command, cancellationToken);

        return Results.NoContent();
    }
}