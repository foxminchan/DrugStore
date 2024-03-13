using DrugStore.Domain.BasketAggregate;
using DrugStore.Domain.IdentityAggregate.Primitives;

namespace DrugStore.WebAPI.Endpoints.Basket;

public sealed class CreateBasketRequest(string idempotency, CreateBasketPayload basket)
{
    public string Idempotency { get; set; } = idempotency;
    public CreateBasketPayload Basket { get; set; } = basket;
}

public sealed class CreateBasketPayload
{
    public IdentityId Id { get; set; }
    public BasketItem Item { get; set; } = default!;
}