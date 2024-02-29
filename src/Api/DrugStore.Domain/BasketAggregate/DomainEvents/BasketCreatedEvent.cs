using DrugStore.Domain.SharedKernel;

namespace DrugStore.Domain.BasketAggregate.DomainEvents;

public sealed class BasketCreatedEvent(Guid productId, int quantity) : DomainEventBase
{
    public Guid ProductId { get; set; } = productId;
    public int Quantity { get; set; } = quantity;
}
