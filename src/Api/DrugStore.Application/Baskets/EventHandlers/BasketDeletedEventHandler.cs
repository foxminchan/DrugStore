using Ardalis.GuardClauses;

using DrugStore.Domain.BasketAggregate.DomainEvents;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Persistence;

using MediatR;

namespace DrugStore.Application.Baskets.EventHandlers;

public sealed class BasketDeletedEventHandler(Repository<Product> repository)
    : INotificationHandler<BasketDeletedEvent>
{
    public async Task Handle(BasketDeletedEvent notification, CancellationToken cancellationToken)
    {
        foreach (KeyValuePair<Guid, int> item in notification.Items)
        {
            Product? product = await repository.GetByIdAsync(item.Key, cancellationToken);
            Guard.Against.NotFound(item.Key, product);
            product.AddStock(item.Value);
            await repository.UpdateAsync(product, cancellationToken);
        }
    }
}
