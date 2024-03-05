using Ardalis.Result;
using DrugStore.Application.Categories.ViewModels;
using DrugStore.Domain.CategoryAggregate;
using DrugStore.Domain.SharedKernel;
using DrugStore.Persistence;

namespace DrugStore.Application.Categories.Queries.GetListQuery;

public sealed class GetListQueryHandler(Repository<Category> repository)
    : IQueryHandler<GetListQuery, Result<List<CategoryVm>>>
{
    public async Task<Result<List<CategoryVm>>> Handle(GetListQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.ListAsync(cancellationToken);

        return Result<List<CategoryVm>>.Success([
            ..result.Select(c => new CategoryVm(
                c.Id,
                c.Name,
                c.Description
            ))
        ]);
    }
}