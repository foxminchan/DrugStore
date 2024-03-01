using DrugStore.Domain.OrderAggregate.DomainEvents;
using DrugStore.Infrastructure.Cache.Redis;
using MediatR;

namespace DrugStore.Application.Orders.EventHandlers;

public sealed class OrderCreatedEventHandler(IRedisService redisService)
    : INotificationHandler<OrderCreatedEvent>
{
    public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        redisService.Remove(notification.Key);
        return Task.CompletedTask;
    }
}