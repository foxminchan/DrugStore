using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Application.Baskets.ViewModels;
using DrugStore.Domain.BasketAggregate;
using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure.Cache.Redis;

namespace DrugStore.Application.Baskets.Queries.GetByUserIdQuery;

public sealed class GetByUserIdQueryHandler(IRedisService redisService)
    : IQueryHandler<GetByUserIdQuery, Result<CustomerBasketVm>>
{
    public Task<Result<CustomerBasketVm>> Handle(GetByUserIdQuery request, CancellationToken cancellationToken)
    {
        var key = $"user:{request.CustomerId}:cart";
        var basket = redisService.HashGetOrSet<CustomerBasket>(
            key,
            request.CustomerId.Value.ToString(),
            () => new()
        );

        Guard.Against.NotFound(key, basket);

        return Task.FromResult(
            Result<CustomerBasketVm>.Success(
                new(
                    basket.Id,
                    [
                        .. basket.Items
                            .Select(x => new BasketItemVm(
                                x.Id,
                                x.ProductName,
                                x.Quantity,
                                x.Price,
                                x.Price * x.Quantity
                            ))
                    ],
                    basket.Items.Sum(x => x.Price * x.Quantity)
                ))
        );
    }
}