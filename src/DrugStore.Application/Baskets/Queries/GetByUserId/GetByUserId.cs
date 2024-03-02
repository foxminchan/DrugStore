using Ardalis.Result;
using DrugStore.Application.Baskets.ViewModels;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Baskets.Queries.GetByUserId;

public sealed record GetByUserId(IdentityId CustomerId, BaseFilter Filter) : IQuery<Result<BasketVm>>;