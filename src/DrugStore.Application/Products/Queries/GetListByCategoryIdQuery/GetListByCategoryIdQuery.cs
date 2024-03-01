using Ardalis.Result;
using DrugStore.Application.Products.ViewModels;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Products.Queries.GetListByCategoryIdQuery;

public sealed record GetListByCategoryIdQuery(Guid CategoryId, BaseFilter Filter)
    : IQuery<PagedResult<List<ProductVm>>>;