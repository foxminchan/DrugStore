using Ardalis.Result;
using DrugStore.Application.Categories.ViewModels;
using DrugStore.Domain.CategoryAggregate;
using DrugStore.Persistence;
using Mapster;
using MediatR;

namespace DrugStore.Application.Categories.Queries.GetListQuery;

public sealed class GetListQueryHandler(Repository<Category> repository)
    : IRequestHandler<GetListQuery, Result<List<CategoryVm>>>
{
    public async Task<Result<List<CategoryVm>>> Handle(GetListQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.ListAsync(cancellationToken);
        return Result<List<CategoryVm>>.Success(result.Adapt<List<CategoryVm>>());
    }
}