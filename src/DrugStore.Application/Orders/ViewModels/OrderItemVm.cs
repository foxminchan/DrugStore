using DrugStore.Domain.OrderAggregate.Primitives;
using DrugStore.Domain.ProductAggregate.Primitives;

namespace DrugStore.Application.Orders.ViewModels;

public sealed record OrderItemVm(
    ProductId? ProductId,
    OrderId? OrderId,
    int Quantity,
    decimal Price,
    decimal Total
);