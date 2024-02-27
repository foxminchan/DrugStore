using DrugStore.Domain.Order;

namespace DrugStore.Application.Orders.ViewModel;

public record OrderVm(
    string? Code,
    OrderStatus Status,
    PaymentMethod PaymentMethod,
    Guid? CustomerId,
    DateTime CreatedDate,
    DateTime? UpdateDate,
    Guid Version);
