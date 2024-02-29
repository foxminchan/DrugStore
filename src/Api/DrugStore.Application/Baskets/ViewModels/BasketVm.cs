using Ardalis.Result;

using DrugStore.Domain.BasketAggregate;

namespace DrugStore.Application.Baskets.ViewModels;

public sealed record BasketVm(Guid CustomerId, PagedResult<List<BasketItem>> Items);
