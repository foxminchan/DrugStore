using DrugStore.Domain.BasketAggregate.DomainEvents;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Domain.ProductAggregate.Primitives;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Domain.BasketAggregate;

public sealed class CustomerBasket : EntityBase, IAggregateRoot
{
    public IdentityId Id { get; set; } = new();
    public List<BasketItem> Items { get; set; } = [];

    public void AddItem(BasketItem basketItem)
        => RegisterDomainEvent(new BasketCreatedEvent(basketItem.Id, basketItem.Quantity));

    public void UpdateItem(BasketItem basketItem)
        => RegisterDomainEvent(new BasketUpdatedEvent(basketItem.Id, basketItem.Quantity));

    public void RemoveItems(IDictionary<ProductId, int> items)
        => RegisterDomainEvent(new BasketDeletedEvent(items));
}