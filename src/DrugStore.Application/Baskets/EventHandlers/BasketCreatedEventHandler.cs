using Ardalis.GuardClauses;
using DrugStore.Domain.BasketAggregate.DomainEvents;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Persistence;
using MediatR;

namespace DrugStore.Application.Baskets.EventHandlers;

public sealed class BasketCreatedEventHandler(Repository<Product> repository)
    : INotificationHandler<BasketCreatedEvent>
{
    public async Task Handle(BasketCreatedEvent notification, CancellationToken cancellationToken)
    {
        var product = await repository.GetByIdAsync(notification.ProductId, cancellationToken);
        Guard.Against.NotFound(notification.ProductId, product);
        product.RemoveStock(notification.Quantity);
        await repository.UpdateAsync(product, cancellationToken);
    }
}