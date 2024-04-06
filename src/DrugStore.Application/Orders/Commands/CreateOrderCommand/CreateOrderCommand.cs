using Ardalis.Result;
using DrugStore.Application.Abstractions.Commands;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Domain.OrderAggregate.Primitives;
using DrugStore.Domain.ProductAggregate.Primitives;

namespace DrugStore.Application.Orders.Commands.CreateOrderCommand;

public sealed record OrderItemCreateRequest(ProductId Id, int Quantity, decimal Price);

public sealed record CreateOrderCommand(
    Guid RequestId,
    string? Code,
    IdentityId? CustomerId,
    List<OrderItemCreateRequest> Items) : IdempotencyCommand<Result<OrderId>>(RequestId);