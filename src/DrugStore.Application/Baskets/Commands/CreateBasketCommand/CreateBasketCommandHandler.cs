using Ardalis.Result;
using DrugStore.Domain.BasketAggregate;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure.Cache.Redis;
using Medallion.Threading;

namespace DrugStore.Application.Baskets.Commands.CreateBasketCommand;

public sealed class CreateBasketCommandHandler(
    IRedisService redisService,
    IDistributedLockProvider distributedLockProvider) : IIdempotencyCommandHandler<CreateBasketCommand, Result<IdentityId>>
{
    public async Task<Result<IdentityId>> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
    {
        BasketItem basketItem = new(
            request.Item.Id,
            request.Item.ProductName,
            request.Item.Quantity,
            request.Item.Price
        );

        var key = $"user:{request.CustomerId}:cart";
        var basket = redisService.Get<CustomerBasket>(key) ?? new CustomerBasket { Id = request.CustomerId };

        basket.Items.Add(basketItem);

        await using (await distributedLockProvider.TryAcquireLockAsync(key, cancellationToken: cancellationToken))
        {
            redisService.HashGetOrSet(key, request.CustomerId.ToString(), () => basket);
            basket.AddItem(basketItem);
        }

        return Result<IdentityId>.Success(basket.Id);
    }
}