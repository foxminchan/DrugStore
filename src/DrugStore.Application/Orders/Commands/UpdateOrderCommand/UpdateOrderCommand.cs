using Ardalis.Result;
using DrugStore.Application.Orders.ViewModels;
using DrugStore.Domain.OrderAggregate;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Orders.Commands.UpdateOrderCommand;

public sealed record OrderItemCreateRequest(Guid? Id, int Quantity, decimal Price);

public sealed record UpdateOrderCommand(
    Guid Id,
    string? Code,
    OrderStatus Status,
    PaymentMethod PaymentMethod,
    Guid? CustomerId,
    List<OrderItemCreateRequest> Items) : ICommand<Result<OrderVm>>;