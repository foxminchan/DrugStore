using DrugStore.Application.Orders.Commands.CreateOrderCommand;
using DrugStore.Domain.IdentityAggregate.Primitives;

namespace DrugStore.WebAPI.Endpoints.Order;

public sealed class CreateOrderRequest(string idempotency, CreateOrderPayload order)
{
    public string Idempotency { get; set; } = idempotency;
    public CreateOrderPayload Order { get; set; } = order;
}

public sealed class CreateOrderPayload
{
    public string? Code { get; set; }
    public IdentityId? CustomerId { get; set; }
    public List<OrderItemCreateRequest> Items { get; set; } = [];
}