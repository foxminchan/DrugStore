using Ardalis.GuardClauses;
using DrugStore.Domain.BasketAggregate.DomainEvents;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Persistence.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DrugStore.Application.Baskets.EventHandlers;

public sealed class BasketCreatedEventHandler(
    IRepository<Product> repository,
    ILogger<BasketCreatedEventHandler> logger) : INotificationHandler<BasketCreatedEvent>
{
    public async Task Handle(BasketCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("[{Event}] Request to remove stock from product with ID: {ProductId}",
            nameof(BasketCreatedEvent), notification.ProductId);
        var product = await repository.GetByIdAsync(notification.ProductId, cancellationToken);
        Guard.Against.NotFound(notification.ProductId, product);
        product.RemoveStock(notification.Quantity);
        await repository.UpdateAsync(product, cancellationToken);
    }
}