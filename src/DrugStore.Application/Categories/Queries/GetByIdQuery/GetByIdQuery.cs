using Ardalis.Result;
using DrugStore.Application.Categories.ViewModels;
using DrugStore.Domain.CategoryAggregate.Primitives;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Categories.Queries.GetByIdQuery;

public sealed record GetByIdQuery(CategoryId Id) : IQuery<Result<CategoryVm>>;