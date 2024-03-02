using Ardalis.Result;
using DrugStore.Application.Categories.ViewModels;
using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure.Cache;

namespace DrugStore.Application.Categories.Queries.GetListQuery;

public sealed record GetListQuery : IQuery<Result<List<CategoryVm>>>, ICachedRequest
{
    public string CacheKey => "Categories";
    public TimeSpan CacheDuration => TimeSpan.FromDays(1);
}