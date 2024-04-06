using Ardalis.GuardClauses;
using DrugStore.Domain.BasketAggregate.DomainEvents;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Persistence.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DrugStore.Application.Baskets.EventHandlers;

public sealed class BasketUpdatedEventHandler(
    IRepository<Product> repository,
    ILogger<BasketUpdatedEventHandler> logger) : INotificationHandler<BasketUpdatedEvent>
{
    public async Task Handle(BasketUpdatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("[{Event}] Request to remove stock from product with ID: {ProductId}",
            nameof(BasketUpdatedEvent), notification.ProductId);
        var product = await repository.GetByIdAsync(notification.ProductId, cancellationToken);
        Guard.Against.NotFound(notification.ProductId, product);
        product.RemoveStock(notification.Quantity);
        await repository.UpdateAsync(product, cancellationToken);
    }
}