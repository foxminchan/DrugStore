using DrugStore.Domain.IdentityAggregate.Primitives;

namespace DrugStore.WebAPI.Endpoints.Basket;

public sealed record CustomerBasketDto(IdentityId Id, List<BasketItemDto> Items, decimal Total);