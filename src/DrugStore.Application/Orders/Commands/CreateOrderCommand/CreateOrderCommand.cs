using Ardalis.Result;
using DrugStore.Domain.OrderAggregate.Enums;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Orders.Commands.CreateOrderCommand;

public sealed record OrderItemCreateRequest(Guid? Id, int Quantity, decimal Price);

public sealed record OrderCreateRequest(
    string? Code,
    OrderStatus Status,
    PaymentMethod PaymentMethod,
    Guid? CustomerId,
    List<OrderItemCreateRequest> Items);

public sealed record CreateOrderCommand(Guid RequestId, OrderCreateRequest Request) 
    : IdempotencyCommand<Result<Guid>>(RequestId);