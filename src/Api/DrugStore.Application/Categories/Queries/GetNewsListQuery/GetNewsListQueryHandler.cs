using Ardalis.GuardClauses;
using Ardalis.Result;

using DrugStore.Application.Categories.ViewModels;
using DrugStore.Domain.CategoryAggregate;
using DrugStore.Domain.CategoryAggregate.Specifications;
using DrugStore.Persistence;

using Mapster;

using MediatR;

namespace DrugStore.Application.Categories.Queries.GetNewsListQuery;

public sealed class GetNewsListQueryHandler(Repository<Category> repository)
    : IRequestHandler<GetNewsListQuery, PagedResult<List<NewsVm>>>
{
    public async Task<PagedResult<List<NewsVm>>> Handle(GetNewsListQuery request, CancellationToken cancellationToken)
    {
        var category =
            await repository.FirstOrDefaultAsync(new CategoryByIdSpec(request.CategoryId), cancellationToken);
        Guard.Against.NotFound(request.CategoryId, category);

        var news = category.News?.AsQueryable();

        if (!string.IsNullOrEmpty(request.Filter.Search))
            news = news?.Where(n => n.Title!.Contains(request.Filter.Search));

        if (request.Filter.OrderBy is { })
            news = request.Filter.IsAscending
                ? news?.OrderBy(n => n.GetType().GetProperty(request.Filter.OrderBy)!.GetValue(n, null))
                : news?.OrderByDescending(n => n.GetType().GetProperty(request.Filter.OrderBy)!.GetValue(n, null));

        news = news?
            .Skip(request.Filter.PageNumber * request.Filter.PageSize)
            .Take(request.Filter.PageSize);

        var totalRecords = news?.Count() ?? 0;
        var totalPages = (int)Math.Ceiling(totalRecords / (double)request.Filter.PageSize);
        var pageInfo = new PagedInfo(request.Filter.PageNumber, request.Filter.PageSize, totalPages, totalRecords);
        return new(pageInfo, news?.Adapt<List<NewsVm>>() ?? []);
    }
}
