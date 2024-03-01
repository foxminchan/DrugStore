using Ardalis.Result;
using DrugStore.Domain.BasketAggregate;
using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure.Cache.Redis;

namespace DrugStore.Application.Baskets.Commands.CreateBasketCommand;

public sealed class CreateBasketCommandHandler(IRedisService redisService)
    : IIdempotencyCommandHandler<CreateBasketCommand, Result<Guid>>
{
    public Task<Result<Guid>> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
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
        redisService.HashGetOrSet(key, request.BasketRequest.CustomerId.ToString(), () => basket);
        basket.AddItem(basketItem);

        return Task.FromResult(Result<Guid>.Success(basket.Id));
    }
}