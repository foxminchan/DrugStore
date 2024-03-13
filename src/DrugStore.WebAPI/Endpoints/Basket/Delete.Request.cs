using DrugStore.Domain.IdentityAggregate.Primitives;

namespace DrugStore.WebAPI.Endpoints.Basket;

public sealed class DeleteBasketRequest(IdentityId id)
{
    public IdentityId Id { get; set; } = id;
}