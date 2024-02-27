using Ardalis.Result;
using DrugStore.Application.Orders.ViewModel;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Orders.Queries.GetListByUserIdQuery;

public sealed record GetListByUserIdQuery(Guid UserId, BaseFilter Filter) : IQuery<PagedResult<IEnumerable<OrderVm>>>;
