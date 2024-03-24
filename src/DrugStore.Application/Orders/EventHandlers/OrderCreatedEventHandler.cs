using DrugStore.Domain.OrderAggregate.DomainEvents;
using DrugStore.Infrastructure.Cache.Redis;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DrugStore.Application.Orders.EventHandlers;

public sealed class OrderCreatedEventHandler(IRedisService redisService, ILogger<OrderCreatedEventHandler> logger)
    : INotificationHandler<OrderCreatedEvent>
{
    public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("[{Event}] Delete cache key: {Key}", nameof(OrderCreatedEvent), notification.Key);
        redisService.Remove(notification.Key);
        return Task.CompletedTask;
    }
}