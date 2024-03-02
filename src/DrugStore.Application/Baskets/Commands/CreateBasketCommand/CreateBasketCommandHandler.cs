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
            request.BasketRequest.Item.Id,
            request.BasketRequest.Item.ProductName,
            request.BasketRequest.Item.Quantity,
            request.BasketRequest.Item.Price
        );

        var key = $"user:{request.BasketRequest.CustomerId}:cart";
        var basket = redisService.Get<CustomerBasket>(key) ??
                     new CustomerBasket { Id = request.BasketRequest.CustomerId };

        basket.Items.Add(basketItem);

        await using (await distributedLockProvider.TryAcquireLockAsync(key, cancellationToken: cancellationToken))
        {
            redisService.HashGetOrSet(key, request.BasketRequest.CustomerId.ToString(), () => basket);
            basket.AddItem(basketItem);
        }

        return Result<IdentityId>.Success(basket.Id);
    }
}