using Ardalis.GuardClauses;
using DrugStore.Domain.ProductAggregate.Primitives;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Domain.BasketAggregate.DomainEvents;

public sealed class BasketCreatedEvent(ProductId productId, int quantity) : DomainEventBase
{
    public ProductId ProductId { get; set; } = productId;
    public int Quantity { get; set; } = Guard.Against.NegativeOrZero(quantity);
}