using Ardalis.Result;
using DrugStore.Application.Products.ViewModel;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Products.Queries.GetListQuery;

public sealed record GetListQuery(BaseFilter Filter) : IQuery<PagedResult<IEnumerable<ProductVm>>>;
