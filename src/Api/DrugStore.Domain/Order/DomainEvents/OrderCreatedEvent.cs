using DrugStore.Domain.SharedKernel;

namespace DrugStore.Domain.Order.DomainEvents;

public sealed class OrderCreatedEvent(Dictionary<Guid, int> products) : DomainEventBase
{
    public Dictionary<Guid, int> Products { get; set; } = products;
}
