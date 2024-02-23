using DrugStore.Domain.SharedKernel;

namespace DrugStore.Domain.Order;

public sealed class OrderItem(
    Guid productId,
    Guid orderId,
    decimal price,
    int quantity) : AuditableEntityBase
{
    public decimal Price { get; set; } = price;
    public int Quantity { get; set; } = quantity;
    public Guid ProductId { get; set; } = productId;
    public Product.Product? Product { get; set; }
    public Guid OrderId { get; set; } = orderId;
    public Order? Order { get; set; }
}
