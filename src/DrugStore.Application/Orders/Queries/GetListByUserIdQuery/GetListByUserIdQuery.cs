using Ardalis.Result;
using DrugStore.Application.Abstractions.Queries;
using DrugStore.Application.Orders.ViewModels;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Persistence.Helpers;

namespace DrugStore.Application.Orders.Queries.GetListByUserIdQuery;

public sealed record GetListByUserIdQuery(IdentityId UserId, PagingHelper Filter)
    : IQuery<PagedResult<List<OrderDetailVm>>>;