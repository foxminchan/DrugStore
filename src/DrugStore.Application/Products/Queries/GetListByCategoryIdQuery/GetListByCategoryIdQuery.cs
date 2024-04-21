using Ardalis.Result;
using DrugStore.Application.Products.ViewModels;
using DrugStore.Domain.CategoryAggregate.Primitives;
using DrugStore.Domain.SharedKernel;
using DrugStore.Persistence.Helpers;

namespace DrugStore.Application.Products.Queries.GetListByCategoryIdQuery;

public sealed record GetListByCategoryIdQuery(CategoryId CategoryId, PagingHelper Filter)
    : IQuery<PagedResult<List<ProductVm>>>;