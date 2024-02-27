using Ardalis.Result;
using DrugStore.Application.Orders.ViewModel;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Orders.Queries.GetListQuery;

public record GetListQuery(BaseFilter Filter) : IQuery<PagedResult<IEnumerable<OrderVm>>>;
