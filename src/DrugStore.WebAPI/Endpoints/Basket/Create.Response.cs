using DrugStore.Domain.IdentityAggregate.Primitives;

namespace DrugStore.WebAPI.Endpoints.Basket;

public class CreateBasketResponse(IdentityId id)
{
    public IdentityId Id { get; set; } = id;
}