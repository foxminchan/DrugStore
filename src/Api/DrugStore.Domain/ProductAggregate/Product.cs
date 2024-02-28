using System.Text.Json.Serialization;

using Ardalis.GuardClauses;

using DrugStore.Domain.CategoryAggregate;
using DrugStore.Domain.OrderAggregate;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Domain.ProductAggregate;

public sealed class Product : AuditableEntityBase, IAggregateRoot
{
    public string? Title { get; set; }
    public string? ProductCode { get; set; }
    public string? Detail { get; set; }
    public ProductStatus Status { get; set; }
    public int Quantity { get; set; }
    public Guid? CategoryId { get; set; }
    public ProductPrice? Price { get; set; }
    [JsonIgnore] public Category? Category { get; set; }
    public ICollection<ProductImage>? Images { get; set; } = [];
    public ICollection<OrderItem>? OrderItems { get; set; } = [];

    /// <summary>
    /// EF mapping constructor
    /// </summary>
    public Product()
    {
    }

    public Product(
        string title,
        string? productCode,
        string? detail,
        bool status,
        int quantity,
        Guid? categoryId,
        decimal originalPrice,
        decimal price,
        decimal? priceSale)
    {
        Title = Guard.Against.NullOrEmpty(title);
        ProductCode = productCode;
        Detail = detail;
        Status = status ? ProductStatus.InStock : ProductStatus.OutOfStock;
        Quantity = Guard.Against.NegativeOrZero(quantity);
        CategoryId = categoryId;
        Price = new(originalPrice, price, priceSale);
    }

    public void Update(string title,
        string? productCode,
        string? detail,
        bool status,
        int quantity,
        Guid? categoryId,
        decimal originalPrice,
        decimal price,
        decimal? priceSale)
    {
        Title = Guard.Against.NullOrEmpty(title);
        ProductCode = productCode;
        Detail = detail;
        Status = status ? ProductStatus.InStock : ProductStatus.OutOfStock;
        Quantity = Guard.Against.NegativeOrZero(quantity);
        CategoryId = categoryId;
        Price = new(originalPrice, price, priceSale);
    }

    public int RemoveStock(int quantityDesired)
    {
        if (quantityDesired <= 0)
            throw new InvalidOperationException("Quantity must be greater than 0");

        if (Status == ProductStatus.OutOfStock)
            throw new InvalidOperationException("Product is out of stock");

        if (Quantity < quantityDesired)
            throw new InvalidOperationException("Product is out of stock");

        Quantity -= quantityDesired;
        return Quantity;
    }

    public void Disable()
    {
        if (Status == ProductStatus.OutOfStock)
            throw new InvalidOperationException("Product is already disabled");

        Status = ProductStatus.OutOfStock;
    }
}
