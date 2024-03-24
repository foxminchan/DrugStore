using DrugStore.Domain.BasketAggregate;
using DrugStore.Domain.ProductAggregate.DomainEvents;
using DrugStore.Infrastructure.Cache.Redis;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DrugStore.Application.Products.EventHandlers;

public sealed class ProductDisabledEventHandler(IRedisService redisService, ILogger<ProductDisabledEventHandler> logger)
    : INotificationHandler<ProductDisabledEvent>
{
    public Task Handle(ProductDisabledEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("[{Event}] Product disabled: {ProductId}", nameof(ProductDisabledEvent),
            notification.ProductId);

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