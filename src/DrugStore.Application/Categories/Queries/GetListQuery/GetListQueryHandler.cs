using Ardalis.Result;
using DrugStore.Application.Categories.ViewModels;
using DrugStore.Domain.CategoryAggregate;
using DrugStore.Domain.SharedKernel;
using DrugStore.Persistence.Repositories;
using MapsterMapper;

namespace DrugStore.Application.Categories.Queries.GetListQuery;

public sealed class GetListQueryHandler(IMapper mapper, IReadRepository<Category> repository)
    : IQueryHandler<GetListQuery, Result<List<CategoryVm>>>
{
    public async Task<Result<List<CategoryVm>>> Handle(GetListQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.ListAsync(cancellationToken);
        return Result<List<CategoryVm>>.Success(mapper.Map<List<CategoryVm>>(result));
    }
}