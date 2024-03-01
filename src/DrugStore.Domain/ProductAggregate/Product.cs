using System.Text.Json.Serialization;
using Ardalis.GuardClauses;
using DrugStore.Domain.CategoryAggregate;
using DrugStore.Domain.OrderAggregate;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Domain.ProductAggregate;

public sealed class Product : AuditableEntityBase, IAggregateRoot
{
    /// <summary>
    ///     EF mapping constructor
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
        ProductPrice productPrice)
    {
        Title = Guard.Against.NullOrEmpty(title);
        ProductCode = productCode;
        Detail = detail;
        Status = status ? ProductStatus.InStock : ProductStatus.OutOfStock;
        Quantity = Guard.Against.NegativeOrZero(quantity);
        CategoryId = categoryId;
        Price = productPrice;
    }

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

    public void Update(string title,
        string? productCode,
        string? detail,
        bool status,
        int quantity,
        Guid? categoryId,
        ProductPrice productPrice)
    {
        Title = Guard.Against.NullOrEmpty(title);
        ProductCode = productCode;
        Detail = detail;
        Status = status ? ProductStatus.InStock : ProductStatus.OutOfStock;
        Quantity = Guard.Against.NegativeOrZero(quantity);
        CategoryId = categoryId;
        Price = productPrice;
    }

    public void RemoveStock(int quantityDesired)
    {
        Quantity -= Guard.Against.NegativeOrZero(quantityDesired);

        if (Status == ProductStatus.InStock && Quantity == 0) Status = ProductStatus.OutOfStock;
    }

    public void AddStock(int quantityDesired)
    {
        Quantity += Guard.Against.NegativeOrZero(quantityDesired);

        if (Status == ProductStatus.OutOfStock && Quantity > 0) Status = ProductStatus.InStock;
    }
}