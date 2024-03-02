using DrugStore.Domain.ProductAggregate.Primitives;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Domain.BasketAggregate.DomainEvents;

public sealed class BasketDeletedEvent(IDictionary<ProductId, int> items) : DomainEventBase
{
    public IDictionary<ProductId, int> Items { get; set; } = items;
}