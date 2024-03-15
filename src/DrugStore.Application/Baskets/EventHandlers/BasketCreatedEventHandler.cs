using Ardalis.GuardClauses;
using DrugStore.Domain.BasketAggregate.DomainEvents;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.SharedKernel;
using MediatR;

namespace DrugStore.Application.Baskets.EventHandlers;

public sealed class BasketCreatedEventHandler(IRepository<Product> repository)
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