using Ardalis.GuardClauses;

using DrugStore.Domain.OrderAggregate.DomainEvents;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Persistence;

using MediatR;

namespace DrugStore.Application.Orders.EventHandlers;

public sealed class OrderCreatedEventHandler(Repository<Product> repository)
    : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        foreach (var item in notification.Products)
        {
            var product = await repository.GetByIdAsync(item.Key, cancellationToken);
            Guard.Against.NotFound(item.Key, product);
            product.RemoveStock(item.Value);
            if (product.Quantity == 0)
            {
                product.Disable();
            }
            await repository.UpdateAsync(product, cancellationToken);
        }
    }
}
