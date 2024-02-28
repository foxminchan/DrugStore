using Ardalis.Result;
using DrugStore.Application.News.ViewModel;
using DrugStore.Domain.News.Specifications;
using DrugStore.Domain.SharedKernel;
using DrugStore.Persistence;
using Mapster;

namespace DrugStore.Application.News.Queries.GetListQuery;

public sealed class GetListQueryHandler(Repository<Domain.News.News> repository)
    : IQueryHandler<GetListQuery, PagedResult<List<NewsVm>>>
{
    public async Task<PagedResult<List<NewsVm>>> Handle(GetListQuery request, CancellationToken cancellationToken)
    {
        var spec = new NewsFilterSpec(
            request.Filter.PageNumber,
            request.Filter.PageSize,
            request.Filter.IsAscending,
            request.Filter.OrderBy,
            request.Filter.Search
        );

        var entities = await repository.ListAsync(spec, cancellationToken);
        var totalRecords = await repository.CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling(totalRecords / (double)request.Filter.PageSize);
        var pageInfo = new PagedInfo(request.Filter.PageNumber, request.Filter.PageSize, totalPages, totalRecords);
        return new(pageInfo, entities.Adapt<List<NewsVm>>());
    }
}
