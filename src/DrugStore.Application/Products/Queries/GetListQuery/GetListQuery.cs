using Ardalis.Result;
using DrugStore.Application.Products.ViewModels;
using DrugStore.Domain.SharedKernel;
using DrugStore.Persistence.Helpers;

namespace DrugStore.Application.Products.Queries.GetListQuery;

public sealed record GetListQuery(FilterHelper Filter) : IQuery<PagedResult<List<ProductVm>>>;