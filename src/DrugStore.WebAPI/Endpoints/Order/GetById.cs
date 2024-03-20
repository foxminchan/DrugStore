using DrugStore.Application.Orders.Queries.GetByIdQuery;
using DrugStore.Domain.OrderAggregate.Primitives;
using DrugStore.WebAPI.Extensions;
using MediatR;

namespace DrugStore.WebAPI.Endpoints.Order;

public sealed class GetById(ISender sender) : IEndpoint<OrderDetailDto, GetOrderByIdRequest>
{
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapGet("/orders/{id}", async (OrderId id) => await HandleAsync(new(id)))
            .WithTags(nameof(Order))
            .WithName("Get Order by Id")
            .Produces<OrderDetailDto>()
            .MapToApiVersion(new(1, 0))
            .RequirePerUserRateLimit();

    public async Task<OrderDetailDto> HandleAsync(
        GetOrderByIdRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await sender.Send(new GetByIdQuery(request.Id), cancellationToken);
        var order = result.Value.Order;
        return new(
            new(order.Id, order.Code, order.Customer?.FullName, order.Total),
            [
                .. result.Value.Items.Select(x
                    => new OrderItemDto(x.ProductId, x.OrderId, x.Quantity, x.Price, x.Total)
                )
            ]
        );
    }
}