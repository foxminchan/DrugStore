using Ardalis.GuardClauses;
using Ardalis.Result;

using DrugStore.Domain.BasketAggregate;
using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure.Cache.Redis;

namespace DrugStore.Application.Baskets.Commands.UpdateBasketCommand;

public sealed class UpdateBasketCommandHandler(IRedisService redisService)
    : ICommandHandler<UpdateBasketCommand, Result<CustomerBasket>>
{
    public Task<Result<CustomerBasket>> Handle(UpdateBasketCommand request, CancellationToken cancellationToken)
    {
        var key = $"user:{request.CustomerId}:cart";
        var basketItem = new BasketItem(
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

        redisService.HashGetOrSet(key, request.CustomerId.ToString(), () => basket);
        basket.UpdateItem(basketItem);
        return Task.FromResult(Result<CustomerBasket>.Success(basket));
    }
}
