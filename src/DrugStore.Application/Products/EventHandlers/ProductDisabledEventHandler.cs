using DrugStore.Domain.BasketAggregate;
using DrugStore.Domain.ProductAggregate.DomainEvents;
using DrugStore.Infrastructure.Cache.Redis;
using MediatR;

namespace DrugStore.Application.Products.EventHandlers;

public sealed class ProductDisabledEventHandler(IRedisService redisService)
    : INotificationHandler<ProductDisabledEvent>
{
    public Task Handle(ProductDisabledEvent notification, CancellationToken cancellationToken)
    {
        var keys = redisService.GetKeys("user:*:cart");

        foreach (var key in keys)
        {
            var basket = redisService.Get<CustomerBasket>(key);
            if (basket is null) continue;
            basket.Items.RemoveAll(x => x.Id == notification.ProductId);
            redisService.HashGetOrSet(key, basket.Id.Value.ToString(), () => basket);
        }

        return Task.CompletedTask;
    }
}