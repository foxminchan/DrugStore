using Ardalis.Result;
using DrugStore.Application.Categories.ViewModel;
using DrugStore.Domain.Category;
using DrugStore.Infrastructure.Cache.Redis;
using DrugStore.Persistence;
using Mapster;
using MediatR;

namespace DrugStore.Application.Categories.Queries.GetListQuery;

public class GetListQueryHandler(Repository<Category> repository, IRedisService redisService)
    : IRequestHandler<GetListQuery, Result<IEnumerable<CategoryVm>>>
{
    public async Task<Result<IEnumerable<CategoryVm>>> Handle(GetListQuery request, CancellationToken cancellationToken)
    {
        var categories = redisService.Get<IEnumerable<CategoryVm>>("categories");

        if (categories is { })
            return Result<IEnumerable<CategoryVm>>.Success(categories);

        var result = await repository.ListAsync(cancellationToken);

        return Result<IEnumerable<CategoryVm>>.Success(result.Adapt<IEnumerable<CategoryVm>>());
    }
}
