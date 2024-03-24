using System.Text.Json;
using Ardalis.GuardClauses;
using DrugStore.Domain.BasketAggregate.DomainEvents;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.SharedKernel;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DrugStore.Application.Baskets.EventHandlers;

public sealed class BasketDeletedEventHandler(
    IRepository<Product> repository,
    ILogger<BasketDeletedEventHandler> logger) : INotificationHandler<BasketDeletedEvent>
{
    public async Task Handle(BasketDeletedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("[{Event}] Request to add stock to products with IDs: {ProductIds}",
            nameof(BasketDeletedEvent), JsonSerializer.Serialize(notification.Items.Keys));

        foreach (var item in notification.Items)
        {
            var product = await repository.GetByIdAsync(item.Key, cancellationToken);
            Guard.Against.NotFound(item.Key, product);
            product.AddStock(item.Value);
            await repository.UpdateAsync(product, cancellationToken);
        }
    }
}