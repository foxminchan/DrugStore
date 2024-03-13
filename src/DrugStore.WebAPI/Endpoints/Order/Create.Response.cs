using DrugStore.Domain.OrderAggregate.Primitives;

namespace DrugStore.WebAPI.Endpoints.Order;

public sealed class CreateOrderResponse(OrderId id)
{
    public OrderId Id { get; set; } = id;
}