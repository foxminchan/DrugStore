using DrugStore.Domain.OrderAggregate.Primitives;

namespace DrugStore.WebAPI.Endpoints.Order;

public sealed record OrderDto(
    OrderId Id,
    string? Code,
    string? CustomerName,
    decimal Total
);