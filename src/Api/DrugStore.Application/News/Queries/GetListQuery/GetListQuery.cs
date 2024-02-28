using Ardalis.Result;
using DrugStore.Application.News.ViewModel;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.News.Queries.GetListQuery;

public sealed record GetListQuery(BaseFilter Filter) : IQuery<PagedResult<List<NewsVm>>>;
