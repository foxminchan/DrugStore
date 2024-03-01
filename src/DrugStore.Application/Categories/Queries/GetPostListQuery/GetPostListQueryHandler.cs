using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Application.Categories.ViewModels;
using DrugStore.Domain.CategoryAggregate;
using DrugStore.Domain.SharedKernel;
using DrugStore.Persistence;
using Mapster;

namespace DrugStore.Application.Categories.Queries.GetPostListQuery;

public sealed class GetPostListQueryHandler(Repository<Category> repository)
    : ICommandHandler<GetPostListQuery, PagedResult<List<PostVm>>>
{
    public async Task<PagedResult<List<PostVm>>> Handle(GetPostListQuery request, CancellationToken cancellationToken)
    {
        var category =
            await repository.GetByIdAsync(request.CategoryId, cancellationToken);
        Guard.Against.NotFound(request.CategoryId, category);

        var posts = category.Posts?.AsQueryable();

        if (!string.IsNullOrEmpty(request.Filter.Search))
            posts = posts?.Where(p => p.Title!.Contains(request.Filter.Search));

        if (request.Filter.OrderBy is { })
            posts = request.Filter.IsAscending
                ? posts?.OrderBy(p => p.GetType().GetProperty(request.Filter.OrderBy)!.GetValue(p, null))
                : posts?.OrderByDescending(p => p.GetType().GetProperty(request.Filter.OrderBy)!.GetValue(p, null));

        posts = posts?.Skip(request.Filter.PageNumber * request.Filter.PageSize)
            .Take(request.Filter.PageSize);

        var totalRecords = posts?.Count() ?? 0;
        var totalPages = (int)Math.Ceiling(totalRecords / (double)request.Filter.PageSize);
        PagedInfo pageInfo = new(request.Filter.PageNumber, request.Filter.PageSize, totalPages, totalRecords);
        return new(pageInfo, posts?.Adapt<List<PostVm>>() ?? []);
    }
}