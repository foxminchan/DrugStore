using Ardalis.GuardClauses;
using Ardalis.Result;

using DrugStore.Application.Categories.ViewModels;
using DrugStore.Domain.CategoryAggregate;
using DrugStore.Domain.SharedKernel;
using DrugStore.Persistence;

using Mapster;

namespace DrugStore.Application.Categories.Queries.GetNewsByIdQuery;

public sealed class GetNewsByIdQueryHandler(Repository<Category> repository)
    : IQueryHandler<GetNewsByIdQuery, Result<NewsVm>>
{
    public async Task<Result<NewsVm>> Handle(GetNewsByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await repository.GetByIdAsync(request.CategoryId, cancellationToken);
        Guard.Against.NotFound(request.CategoryId, category);

        var news = category.News?.FirstOrDefault(n => n.Id == request.NewsId);
        Guard.Against.NotFound(request.NewsId, news);

        return Result<NewsVm>.Success(news.Adapt<NewsVm>());
    }
}
