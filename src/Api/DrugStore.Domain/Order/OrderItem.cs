using Ardalis.GuardClauses;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Domain.Order;

public sealed class OrderItem : AuditableEntityBase
{
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public Guid ProductId { get; set; }
    public Product.Product? Product { get; set; }
    public Guid OrderId { get; set; }
    public Order? Order { get; set; }

    /// <summary>
    /// EF mapping constructor
    /// </summary>
    public OrderItem()
    {
    }

    public OrderItem(decimal price, int quantity, Guid productId, Guid orderId)
    {
        Price = Guard.Against.NegativeOrZero(price);
        Quantity = Guard.Against.NegativeOrZero(quantity);
        ProductId = Guard.Against.Default(productId);
        OrderId = Guard.Against.Default(orderId);
    }
}
