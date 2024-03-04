using Ardalis.GuardClauses;
using DrugStore.Domain.ProductAggregate.Primitives;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Domain.BasketAggregate;

public sealed class BasketItem(ProductId productId, string? productName, int quantity, decimal price) : EntityBase
{
    public ProductId Id { get; private set; } = productId;
    public string? ProductName { get; set; } = productName;
    public int Quantity { get; set; } = Guard.Against.NegativeOrZero(quantity);
    public decimal Price { get; set; } = Guard.Against.NegativeOrZero(price);

    public void Update(ProductId productId, string? productName, int quantity, decimal price)
    {
        Id = productId;
        ProductName = productName;
        Quantity = Guard.Against.NegativeOrZero(quantity);
        Price = Guard.Against.NegativeOrZero(price);
        UpdateDate = DateTime.UtcNow;
    }
}