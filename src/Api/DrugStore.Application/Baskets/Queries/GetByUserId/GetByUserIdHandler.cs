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
        string key = $"user:{request.CustomerId}:cart";
        CustomerBasket basket =
            redisService.HashGetOrSet<CustomerBasket>(key, request.CustomerId.ToString(), () => new());
        Guard.Against.NotFound(key, basket);

        List<BasketItem> items = basket.Items
            .Skip(request.Filter.PageSize * (request.Filter.PageNumber - 1))
            .Take(request.Filter.PageSize)
            .OrderBy(x => x.Price * x.Quantity)
            .ThenByDescending(x => x.CreatedDate)
            .ToList();

        int totalRecords = basket.Items.Count;
        int totalPages = (int)Math.Ceiling(totalRecords / (double)request.Filter.PageSize);
        PagedInfo pagedInfo = new PagedInfo(
            request.Filter.PageNumber,
            request.Filter.PageSize,
            totalPages,
            totalRecords
        );

        PagedResult<List<BasketItem>> pagedResult = new PagedResult<List<BasketItem>>(pagedInfo, items);
        return Task.FromResult(Result<BasketVm>.Success(new(request.CustomerId, pagedResult)));
    }
}
