using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Domain.OrderAggregate.Primitives;
using DrugStore.Domain.ProductAggregate.Primitives;

namespace DrugStore.Application.Orders.ViewModels;

public sealed record OrderVm(
    OrderId Id,
    string? Code,
    IdentityId? CustomerId,
    List<OrderItemVm> Items
);

public sealed record OrderItemVm(
    ProductId? ProductId,
    OrderId? OrderId,
    int Quantity,
    decimal Price
);