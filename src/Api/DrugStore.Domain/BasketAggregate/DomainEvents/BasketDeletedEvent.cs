using DrugStore.Domain.SharedKernel;

namespace DrugStore.Domain.BasketAggregate.DomainEvents;

public sealed class BasketDeletedEvent(IDictionary<Guid, int> items) : DomainEventBase
{
    public IDictionary<Guid, int> Items { get; set; } = items;
}
