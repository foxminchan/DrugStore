using DrugStore.Domain.BasketAggregate.DomainEvents;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Domain.BasketAggregate;

public sealed class CustomerBasket : EntityBase, IAggregateRoot
{
    public List<BasketItem> Items { get; set; } = [];

    public void AddItem(BasketItem basketItem)
        => RegisterDomainEvent(new BasketCreatedEvent(basketItem.Id, basketItem.Quantity));

    public void UpdateItem(BasketItem basketItem)
        => RegisterDomainEvent(new BasketUpdatedEvent(basketItem.Id, basketItem.Quantity));

    public void RemoveItems(IDictionary<Guid, int> items)
        => RegisterDomainEvent(new BasketDeletedEvent(items));
}