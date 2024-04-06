using Ardalis.Result;
using DrugStore.Application.Abstractions.Queries;
using DrugStore.Application.Baskets.ViewModels;
using DrugStore.Domain.IdentityAggregate.Primitives;

namespace DrugStore.Application.Baskets.Queries.GetByUserIdQuery;

public sealed record GetByUserIdQuery(IdentityId CustomerId) : IQuery<Result<CustomerBasketVm>>;