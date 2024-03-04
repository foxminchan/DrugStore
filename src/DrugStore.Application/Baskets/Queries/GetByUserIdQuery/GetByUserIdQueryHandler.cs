using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Domain.BasketAggregate;
using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure.Cache.Redis;

namespace DrugStore.Application.Baskets.Queries.GetByUserIdQuery;

public sealed class GetByUserIdQueryHandler(IRedisService redisService)
    : IQueryHandler<GetByUserIdQuery, Result<CustomerBasket>>
{
    public Task<Result<CustomerBasket>> Handle(GetByUserIdQuery request, CancellationToken cancellationToken)
    {
        var key = $"user:{request.CustomerId}:cart";
        var basket = redisService.HashGetOrSet<CustomerBasket>(
            key,
            request.CustomerId.Value.ToString(),
            () => new()
        );

        Guard.Against.NotFound(key, basket);

        return Task.FromResult(Result<CustomerBasket>.Success(basket));
    }
}