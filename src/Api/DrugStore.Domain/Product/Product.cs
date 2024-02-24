using DrugStore.Domain.SharedKernel;
using System.Text.Json.Serialization;
using DrugStore.Domain.Order;

namespace DrugStore.Domain.Product;

public sealed class Product(
    string? title,
    string? productCode,
    string? detail,
    int quantity,
    bool status,
    Guid? categoryId) : AuditableEntityBase, IAggregateRoot
{
    public string? Title { get; set; } = title;
    public string? ProductCode { get; set; } = productCode;
    public string? Detail { get; set; } = detail;
    public ProductStatus Status { get; set; } = status ? ProductStatus.InStock : ProductStatus.OutOfStock;
    public int Quantity { get; set; } = quantity;
    public Guid? CategoryId { get; set; } = categoryId;
    public ProductPrice? Price { get; set; }
    [JsonIgnore] public Category.Category? Category { get; set; }
    public ICollection<ProductImage>? Images { get; set; } = [];
    public ICollection<OrderItem>? OrderItems { get; set; } = [];

    public int RemoveStock(int quantityDesired)
    {
        if (quantityDesired <= 0)
        {
            throw new InvalidOperationException("Quantity must be greater than 0");
        }

        if (Status == ProductStatus.OutOfStock)
        {
            throw new InvalidOperationException("Product is out of stock");
        }

        if (Quantity < quantityDesired)
        {
            throw new InvalidOperationException("Product is out of stock");
        }

        Quantity -= quantityDesired;
        return Quantity;
    }
}
