using DrugStore.Domain.OrderAggregate;

namespace DrugStore.Application.Orders.ViewModels;

public sealed record OrderVm(
    Guid Id,
    string? Code,
    OrderStatus Status,
    PaymentMethod PaymentMethod,
    Guid? CustomerId,
    DateTime CreatedDate,
    DateTime? UpdateDate,
    Guid Version,
    List<OrderItemVm> Items);

public sealed record OrderItemVm(
    Guid? ProductId,
    Guid? OrderId,
    int Quantity, 
    decimal Price, 
    DateTime CreatedDate,
    DateTime? UpdateDate,
    Guid Version);