using Ardalis.Result;
using DrugStore.Application.Products.ViewModel;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Products.Queries.GetListByCategoryIdQuery;

public sealed record GetListByCategoryIdQuery(Guid CategoryId, BaseFilter Filter) 
    : IQuery<PagedResult<IEnumerable<ProductVm>>>;
