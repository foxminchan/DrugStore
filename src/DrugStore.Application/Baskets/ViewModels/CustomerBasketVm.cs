using DrugStore.Domain.IdentityAggregate.Primitives;

namespace DrugStore.Application.Baskets.ViewModels;

public sealed record CustomerBasketVm(
    IdentityId Id,
    List<BasketItemVm> Items,
    decimal Total
);