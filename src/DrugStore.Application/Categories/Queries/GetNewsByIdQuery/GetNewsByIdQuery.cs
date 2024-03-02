using Ardalis.Result;
using DrugStore.Application.Categories.ViewModels;
using DrugStore.Domain.CategoryAggregate.Primitives;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Categories.Queries.GetNewsByIdQuery;

public sealed record GetNewsByIdQuery(CategoryId CategoryId, NewsId NewsId) : IQuery<Result<NewsVm>>;