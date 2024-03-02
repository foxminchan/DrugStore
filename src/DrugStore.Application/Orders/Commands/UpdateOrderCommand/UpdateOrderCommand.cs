using Ardalis.Result;
using DrugStore.Application.Orders.ViewModels;
using DrugStore.Domain.OrderAggregate.Enums;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Orders.Commands.UpdateOrderCommand;

public sealed record OrderUpdateRequest(
    Guid Id,
    string? Code,
    OrderStatus Status,
    PaymentMethod PaymentMethod,
    Guid? CustomerId,
    List<OrderItemUpdateRequest> Items);

public sealed record OrderItemUpdateRequest(Guid? Id, int Quantity, decimal Price);

public sealed record UpdateOrderCommand(OrderUpdateRequest Request) : ICommand<Result<OrderVm>>;