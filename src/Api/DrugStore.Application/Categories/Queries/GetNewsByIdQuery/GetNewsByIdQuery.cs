using Ardalis.Result;

using DrugStore.Application.Categories.ViewModels;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Categories.Queries.GetNewsByIdQuery;

public sealed record GetNewsByIdQuery(Guid CategoryId, Guid NewsId) : IQuery<Result<NewsVm>>;
