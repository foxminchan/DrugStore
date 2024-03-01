using Ardalis.Result;
using DrugStore.Application.Orders.ViewModels;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Orders.Queries.GetListQuery;

public sealed record GetListQuery(BaseFilter Filter) : IQuery<PagedResult<List<OrderVm>>>;