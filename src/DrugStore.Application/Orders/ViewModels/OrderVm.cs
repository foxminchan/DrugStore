using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Domain.OrderAggregate.Enums;
using DrugStore.Domain.OrderAggregate.Primitives;
using DrugStore.Domain.ProductAggregate.Primitives;

namespace DrugStore.Application.Orders.ViewModels;

public sealed record OrderVm(
    OrderId Id,
    string? Code,
    OrderStatus? Status,
    PaymentMethod? PaymentMethod,
    IdentityId? CustomerId,
    DateTime CreatedDate,
    DateTime? UpdateDate,
    Guid Version,
    List<OrderItemVm> Items);

public sealed record OrderItemVm(
    ProductId? ProductId,
    OrderId? OrderId,
    int Quantity, 
    decimal Price, 
    DateTime CreatedDate,
    DateTime? UpdateDate,
    Guid Version);