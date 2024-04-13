using DrugStore.Application.Orders.Queries.GetByIdQuery;
using DrugStore.Domain.OrderAggregate.Primitives;
using DrugStore.WebAPI.Endpoints.Abstractions;
using DrugStore.WebAPI.Extensions;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.Order;

public sealed class GetById(ISender sender) : IEndpoint<IResult, GetOrderByIdRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/orders/{id}", async (OrderId id) => await HandleAsync(new(id)))
            .Produces<OrderDetailDto>()
            .WithTags(nameof(Order))
            .WithName("Get Order by Id")
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<IResult> HandleAsync(
        GetOrderByIdRequest request,
        CancellationToken cancellationToken = default)
    {
        GetByIdQuery query = new(request.Id);

        var result = await sender.Send(query, cancellationToken);

        var order = result.Value.Order;

        OrderDto orderDto = new(order.Id, order.Code, order.Customer?.FullName, order.Customer!.Id, order.Total);

        var items = result.Value.Items.Select(x
            => new OrderItemDto(x.ProductId, x.OrderId, x.Quantity, x.Price, x.Total)
        ).ToList();

        OrderDetailDto response = new(orderDto, items);

        return Results.Ok(response);
    }
}