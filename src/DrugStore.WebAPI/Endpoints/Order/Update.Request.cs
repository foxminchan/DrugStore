using DrugStore.Application.Orders.Commands.UpdateOrderCommand;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Domain.OrderAggregate.Primitives;

namespace DrugStore.WebAPI.Endpoints.Order;

public sealed class UpdateOrderRequest
{
    public OrderId Id { get; set; }
    public string? Code { get; set; }
    public IdentityId? CustomerId { get; set; }
    public List<OrderItemUpdateRequest> Items { get; set; } = [];
}