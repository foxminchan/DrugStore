using DrugStore.Domain.OrderAggregate;

namespace DrugStore.Application.Orders.ViewModels;

public record OrderVm(
    string? Code,
    OrderStatus Status,
    PaymentMethod PaymentMethod,
    Guid? CustomerId,
    DateTime CreatedDate,
    DateTime? UpdateDate,
    Guid Version);
