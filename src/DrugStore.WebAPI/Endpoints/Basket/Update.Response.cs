namespace DrugStore.WebAPI.Endpoints.Basket;

public sealed class UpdateBasketResponse(CustomerBasketDto basket)
{
    public CustomerBasketDto Basket { get; set; } = basket;
}