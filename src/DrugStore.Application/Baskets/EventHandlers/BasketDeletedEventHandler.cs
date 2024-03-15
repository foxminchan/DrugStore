using Ardalis.GuardClauses;
using DrugStore.Domain.BasketAggregate.DomainEvents;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.SharedKernel;
using MediatR;

namespace DrugStore.Application.Baskets.EventHandlers;

public sealed class BasketDeletedEventHandler(IRepository<Product> repository)
    : INotificationHandler<BasketDeletedEvent>
{
    public async Task Handle(BasketDeletedEvent notification, CancellationToken cancellationToken)
    {
        foreach (var item in notification.Items)
        {
            var product = await repository.GetByIdAsync(item.Key, cancellationToken);
            Guard.Against.NotFound(item.Key, product);
            product.AddStock(item.Value);
            await repository.UpdateAsync(product, cancellationToken);
        }
    }
}