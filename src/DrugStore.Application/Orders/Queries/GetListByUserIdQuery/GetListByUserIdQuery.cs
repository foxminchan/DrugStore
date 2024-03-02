using Ardalis.Result;
using DrugStore.Application.Orders.ViewModels;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Orders.Queries.GetListByUserIdQuery;

public sealed record GetListByUserIdQuery(IdentityId UserId, BaseFilter Filter) : IQuery<PagedResult<List<OrderVm>>>;