using DrugStore.Domain.OrderAggregate.Primitives;

namespace DrugStore.WebAPI.Endpoints.Order;

public sealed class GetOrderByIdRequest(OrderId id)
{
    public OrderId Id { get; set; } = id;
}