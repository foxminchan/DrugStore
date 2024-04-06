using Ardalis.Result;
using DrugStore.Application.Abstractions.Queries;
using DrugStore.Application.Categories.ViewModels;
using DrugStore.Domain.CategoryAggregate.Primitives;

namespace DrugStore.Application.Categories.Queries.GetByIdQuery;

public sealed record GetByIdQuery(CategoryId Id) : IQuery<Result<CategoryVm>>;