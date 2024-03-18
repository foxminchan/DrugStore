using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Application.Baskets.ViewModels;
using DrugStore.Domain.BasketAggregate;
using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure.Cache.Redis;
using MapsterMapper;
using Medallion.Threading;

namespace DrugStore.Application.Baskets.Commands.UpdateBasketCommand;

public sealed class UpdateBasketCommandHandler(
    IMapper mapper,
    IRedisService redisService,
    IDistributedLockProvider distributedLockProvider) : ICommandHandler<UpdateBasketCommand, Result<CustomerBasketVm>>
{
    public async Task<Result<CustomerBasketVm>> Handle(UpdateBasketCommand request, CancellationToken cancellationToken)
    {
        var key = $"user:{request.CustomerId}:cart";
        BasketItem basketItem = new(
            request.Item.Id,
            request.Item.ProductName,
            request.Item.Quantity,
            request.Item.Price
        );

        var basket = redisService.Get<CustomerBasket>(key);
        Guard.Against.NotFound(key, basket);

        var item = basket.Items.Find(x => x.Id == basketItem.Id);
        Guard.Against.NotFound(nameof(basketItem.Id), item);

        if (item.Quantity == 0)
            basket.Items.Remove(item);
        else
            basket.Items[basket.Items.IndexOf(item)].Update(
                basketItem.Id,
                basketItem.ProductName,
                basketItem.Quantity,
                basketItem.Price
            );

        await using (await distributedLockProvider.TryAcquireLockAsync(key, cancellationToken: cancellationToken))
        {
            redisService.HashGetOrSet(key, request.CustomerId.ToString(), () => basket);
            basket.UpdateItem(basketItem);
        }

        return Result<CustomerBasketVm>.Success(mapper.Map<CustomerBasketVm>(basket));
    }
}