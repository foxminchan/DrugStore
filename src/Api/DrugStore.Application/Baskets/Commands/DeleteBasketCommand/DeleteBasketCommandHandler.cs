using Ardalis.GuardClauses;
using Ardalis.Result;

using DrugStore.Domain.BasketAggregate;
using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure.Cache.Redis;

namespace DrugStore.Application.Baskets.Commands.DeleteBasketCommand;

public sealed class DeleteBasketCommandHandler(IRedisService redisService)
    : ICommandHandler<DeleteBasketCommand, Result>
{
    public Task<Result> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        var key = $"user:{request.BasketId}:cart";

        var basket = redisService.Get<CustomerBasket>(key);
        Guard.Against.NotFound(key, basket);

        var basketItems = basket.Items.ToDictionary(x => x.Id, x => x.Quantity);
        redisService.Remove(key);
        basket.RemoveItems(basketItems);

        return Task.FromResult(Result.Success());
    }
}
