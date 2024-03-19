using DrugStore.Domain.ProductAggregate.Primitives;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Domain.BasketAggregate;

public sealed class BasketItem(ProductId productId, string? productName, int quantity, decimal price) : EntityBase
{
    public ProductId Id { get; set; } = productId;
    public string? ProductName { get; set; } = productName;
    public int Quantity { get; set; } = quantity;
    public decimal Price { get; set; } = price;

    public void Update(ProductId productId, string? productName, int quantity, decimal price)
    {
        Id = productId;
        ProductName = productName;
        Quantity = quantity;
        Price = price;
        UpdateDate = DateTime.UtcNow;
    }
}