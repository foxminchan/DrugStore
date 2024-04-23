using DrugStore.Domain.OrderAggregate.Primitives;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.ProductAggregate.Primitives;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Domain.OrderAggregate;

public sealed class OrderItem : EntityBase
{
    /// <summary>
    ///     EF mapping constructor
    /// </summary>
    public OrderItem()
    {
    }

    public OrderItem(decimal price, int quantity, ProductId productId)
    {
        Price = price;
        Quantity = quantity;
        ProductId = productId;
    }

    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public ProductId ProductId { get; set; }
    public Product? Product { get; set; }
    public OrderId OrderId { get; set; }
    public Order? Order { get; set; }
}