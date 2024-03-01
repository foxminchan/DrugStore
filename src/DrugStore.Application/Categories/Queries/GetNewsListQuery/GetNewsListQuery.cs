using Ardalis.Result;
using DrugStore.Application.Categories.ViewModels;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Categories.Queries.GetNewsListQuery;

public sealed record GetNewsListQuery(Guid CategoryId, BaseFilter Filter)
    : IQuery<PagedResult<List<NewsVm>>>;