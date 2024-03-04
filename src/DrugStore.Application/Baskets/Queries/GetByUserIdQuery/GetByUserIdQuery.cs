using Ardalis.Result;
using DrugStore.Domain.BasketAggregate;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Baskets.Queries.GetByUserIdQuery;

public sealed record GetByUserIdQuery(IdentityId CustomerId) : IQuery<Result<CustomerBasket>>;