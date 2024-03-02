using Ardalis.Result;
using DrugStore.Domain.OrderAggregate;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Orders.Commands.CreateOrderCommand;

public sealed record OrderItemCreateRequest(Guid? Id, int Quantity, decimal Price);

public sealed record OrderCreateRequest(
    string? Code,
    OrderStatus Status,
    PaymentMethod PaymentMethod,
    Guid? CustomerId);

public sealed record CreateOrderCommand(
    Guid RequestId,
    OrderCreateRequest Order,
    List<OrderItemCreateRequest> Items) : IdempotencyCommand<Result<Guid>>(RequestId);