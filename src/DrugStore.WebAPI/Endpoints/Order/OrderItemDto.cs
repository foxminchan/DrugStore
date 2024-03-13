using DrugStore.Domain.OrderAggregate.Primitives;
using DrugStore.Domain.ProductAggregate.Primitives;

namespace DrugStore.WebAPI.Endpoints.Order;

public sealed record OrderItemDto(
    ProductId? ProductId,
    OrderId? OrderId,
    int Quantity,
    decimal Price,
    decimal Total
);