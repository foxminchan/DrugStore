using DrugStore.Domain.OrderAggregate.Primitives;

namespace DrugStore.WebAPI.Endpoints.Order;

public class DeleteOrderRequest(OrderId id)
{
    public OrderId Id { get; set; } = id;
}