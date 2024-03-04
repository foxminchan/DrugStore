using System.Text.Json.Serialization;
using Ardalis.GuardClauses;
using DrugStore.Domain.CategoryAggregate;
using DrugStore.Domain.CategoryAggregate.Primitives;
using DrugStore.Domain.OrderAggregate;
using DrugStore.Domain.ProductAggregate.Enums;
using DrugStore.Domain.ProductAggregate.Primitives;
using DrugStore.Domain.ProductAggregate.ValueObjects;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Domain.ProductAggregate;

public sealed class Product : EntityBase, IAggregateRoot
{
    /// <summary>
    ///     EF mapping constructor
    /// </summary>
    public Product()
    {
    }

    public Product(
        string name,
        string? productCode,
        string? detail,
        bool status,
        int quantity,
        CategoryId? categoryId,
        ProductPrice productPrice)
    {
        Name = Guard.Against.NullOrEmpty(name);
        ProductCode = productCode;
        Detail = detail;
        Status = status ? ProductStatus.InStock : ProductStatus.OutOfStock;
        Quantity = Guard.Against.NegativeOrZero(quantity);
        CategoryId = categoryId;
        Price = productPrice;
    }

    public ProductId Id { get; set; } = new(Guid.NewGuid());
    public string? Name { get; set; }
    public string? ProductCode { get; set; }
    public string? Detail { get; set; }
    public ProductStatus? Status { get; set; }
    public int Quantity { get; set; }
    public CategoryId? CategoryId { get; set; }
    public ProductPrice? Price { get; set; }
    [JsonIgnore] public Category? Category { get; set; }
    public ICollection<ProductImage>? Images { get; set; } = [];
    public ICollection<OrderItem>? OrderItems { get; set; } = [];

    public void Update(string title,
        string? productCode,
        string? detail,
        ProductStatus status,
        int quantity,
        CategoryId? categoryId,
        ProductPrice productPrice)
    {
        Name = Guard.Against.NullOrEmpty(title);
        ProductCode = productCode;
        Detail = detail;
        Status = status;
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
