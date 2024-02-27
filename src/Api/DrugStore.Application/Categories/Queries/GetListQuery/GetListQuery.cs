using Ardalis.Result;
using DrugStore.Application.Categories.ViewModel;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Categories.Queries.GetListQuery;

public sealed record GetListQuery : IQuery<Result<List<CategoryVm>>>;
