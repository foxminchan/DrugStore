using Ardalis.Result;
using DrugStore.Domain.BasketAggregate;
using DrugStore.Domain.IdentityAggregate.Primitives;

namespace DrugStore.Application.Baskets.ViewModels;

public sealed record BasketVm(IdentityId CustomerId, PagedResult<List<BasketItem>> Items);