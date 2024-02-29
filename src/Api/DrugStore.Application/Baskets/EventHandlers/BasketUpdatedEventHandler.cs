using Ardalis.GuardClauses;

using DrugStore.Domain.BasketAggregate.DomainEvents;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Persistence;

using MediatR;

namespace DrugStore.Application.Baskets.EventHandlers;

public sealed class BasketUpdatedEventHandler(Repository<Product> repository)
    : INotificationHandler<BasketUpdatedEvent>
{
    public async Task Handle(BasketUpdatedEvent notification, CancellationToken cancellationToken)
    {
        Product? product = await repository.GetByIdAsync(notification.ProductId, cancellationToken);
        Guard.Against.NotFound(notification.ProductId, product);
        product.RemoveStock(notification.Quantity);
        await repository.UpdateAsync(product, cancellationToken);
    }
}
