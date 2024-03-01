using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Application.Baskets.ViewModels;
using DrugStore.Domain.BasketAggregate;
using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure.Cache.Redis;

namespace DrugStore.Application.Baskets.Queries.GetByUserId;

public sealed class GetByUserIdHandler(IRedisService redisService)
    : IQueryHandler<GetByUserId, Result<BasketVm>>
{
    public Task<Result<BasketVm>> Handle(GetByUserId request, CancellationToken cancellationToken)
    {
        var key = $"user:{request.CustomerId}:cart";
        var basket =
            redisService.HashGetOrSet<CustomerBasket>(key, request.CustomerId.ToString(), () => new());
        Guard.Against.NotFound(key, basket);

        var items = basket.Items
            .Skip(request.Filter.PageSize * (request.Filter.PageNumber - 1))
            .Take(request.Filter.PageSize)
            .OrderBy(x => x.Price * x.Quantity)
            .ThenByDescending(x => x.CreatedDate)
            .ToList();

        var totalRecords = basket.Items.Count;
        var totalPages = (int)Math.Ceiling(totalRecords / (double)request.Filter.PageSize);
        PagedInfo pagedInfo = new(
            request.Filter.PageNumber,
            request.Filter.PageSize,
            totalPages,
            totalRecords
        );

        PagedResult<List<BasketItem>> pagedResult = new(pagedInfo, items);
        return Task.FromResult(Result<BasketVm>.Success(new(request.CustomerId, pagedResult)));
    }
}