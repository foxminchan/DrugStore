namespace DrugStore.Domain.Basket;

public sealed class BasketItem
{
    public Guid ProductId { get; set; }
    public string? ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
