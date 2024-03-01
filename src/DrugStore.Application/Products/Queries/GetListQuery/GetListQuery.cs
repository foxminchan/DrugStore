using Ardalis.Result;
using DrugStore.Application.Products.ViewModels;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Products.Queries.GetListQuery;

public sealed record GetListQuery(BaseFilter Filter) : IQuery<PagedResult<List<ProductVm>>>;