using Ardalis.Result;
using DrugStore.Application.Categories.ViewModels;
using DrugStore.Domain.CategoryAggregate;
using DrugStore.Infrastructure.Cache.Redis;
using DrugStore.Persistence;
using Mapster;
using MediatR;

namespace DrugStore.Application.Categories.Queries.GetListQuery;

public sealed class GetListQueryHandler(Repository<Category> repository, IRedisService redisService)
    : IRequestHandler<GetListQuery, Result<List<CategoryVm>>>
{
    public async Task<Result<List<CategoryVm>>> Handle(GetListQuery request, CancellationToken cancellationToken)
    {
        var categories = redisService.Get<List<CategoryVm>>("categories");

        if (categories is { }) return Result<List<CategoryVm>>.Success(categories);

        var result = await repository.ListAsync(cancellationToken);

        return Result<List<CategoryVm>>.Success(result.Adapt<List<CategoryVm>>());
    }
}