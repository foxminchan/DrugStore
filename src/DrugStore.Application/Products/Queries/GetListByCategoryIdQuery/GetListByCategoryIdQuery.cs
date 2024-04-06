using Ardalis.Result;
using DrugStore.Application.Abstractions.Queries;
using DrugStore.Application.Products.ViewModels;
using DrugStore.Domain.CategoryAggregate.Primitives;
using DrugStore.Persistence.Helpers;

namespace DrugStore.Application.Products.Queries.GetListByCategoryIdQuery;

public sealed record GetListByCategoryIdQuery(CategoryId CategoryId, PagingHelper Filter)
    : IQuery<PagedResult<List<ProductVm>>>;