using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Application.Abstractions.Queries;
using DrugStore.Application.Baskets.ViewModels;
using DrugStore.Domain.BasketAggregate;
using DrugStore.Infrastructure.Cache.Redis;
using MapsterMapper;

namespace DrugStore.Application.Baskets.Queries.GetByUserIdQuery;

public sealed class GetByUserIdQueryHandler(IRedisService redisService, IMapper mapper)
    : IQueryHandler<GetByUserIdQuery, Result<CustomerBasketVm>>
{
    public Task<Result<CustomerBasketVm>> Handle(GetByUserIdQuery request, CancellationToken cancellationToken)
    {
        var key = $"user:{request.CustomerId}:cart";
        var basket = redisService.HashGetOrSet<CustomerBasket>(key, request.CustomerId.Value.ToString(), () => new());
        Guard.Against.NotFound(key, basket);
        var result = Result<CustomerBasketVm>.Success(mapper.Map<CustomerBasketVm>(basket));
        return Task.FromResult(result);
    }
}