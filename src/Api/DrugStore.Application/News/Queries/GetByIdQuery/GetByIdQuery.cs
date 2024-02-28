using Ardalis.Result;
using DrugStore.Application.News.ViewModel;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.News.Queries.GetByIdQuery;

public sealed record GetByIdQuery(Guid Id) : IQuery<Result<NewsVm>>;
