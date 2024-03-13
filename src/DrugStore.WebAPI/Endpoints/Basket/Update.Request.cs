using DrugStore.Domain.BasketAggregate;
using DrugStore.Domain.IdentityAggregate.Primitives;

namespace DrugStore.WebAPI.Endpoints.Basket;

public sealed class UpdateBasketRequest
{
    public IdentityId CustomerId { get; set; }
    public BasketItem Item { get; set; } = default!;
}