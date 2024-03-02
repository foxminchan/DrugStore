using Ardalis.Result;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Domain.OrderAggregate.Enums;
using DrugStore.Domain.OrderAggregate.Primitives;
using DrugStore.Domain.ProductAggregate.Primitives;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Orders.Commands.CreateOrderCommand;

public sealed record OrderItemCreateRequest(ProductId Id, int Quantity, decimal Price);

public sealed record OrderCreateRequest(
    string? Code,
    OrderStatus Status,
    PaymentMethod PaymentMethod,
    IdentityId? CustomerId,
    List<OrderItemCreateRequest> Items);

public sealed record CreateOrderCommand(Guid RequestId, OrderCreateRequest Request) 
    : IdempotencyCommand<Result<OrderId>>(RequestId);