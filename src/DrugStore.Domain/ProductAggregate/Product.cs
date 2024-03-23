using Ardalis.GuardClauses;
using DrugStore.Domain.CategoryAggregate;
using DrugStore.Domain.CategoryAggregate.Primitives;
using DrugStore.Domain.OrderAggregate;
using DrugStore.Domain.ProductAggregate.DomainEvents;
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
        int quantity,
        CategoryId? categoryId,
        ProductPrice productPrice,
        ProductImage? image = null)
    {
        Name = Guard.Against.NullOrEmpty(name);
        ProductCode = productCode;
        Detail = detail;
        Status = quantity > 0 ? ProductStatus.InStock : ProductStatus.OutOfStock;
        Quantity = quantity;
        CategoryId = categoryId;
        Price = productPrice;
        Image = image;
    }

    public ProductId Id { get; set; } = new(Guid.NewGuid());
    public string? Name { get; set; }
    public string? ProductCode { get; set; }
    public string? Detail { get; set; }
    public ProductStatus? Status { get; set; }
    public int Quantity { get; set; }
    public ProductImage? Image { get; set; }
    public ProductPrice? Price { get; set; }
    public CategoryId? CategoryId { get; set; }
    public Category? Category { get; set; }
    public ICollection<OrderItem>? OrderItems { get; set; } = [];

    public void Update(
        string name,
        string? productCode,
        string? detail,
        int quantity,
        CategoryId? categoryId,
        ProductPrice productPrice,
        ProductImage? image = null)
    {
        Name = Guard.Against.NullOrEmpty(name);
        ProductCode = productCode;
        Detail = detail;
        Status = quantity > 0 ? ProductStatus.InStock : ProductStatus.OutOfStock;
        Quantity = quantity;
        CategoryId = categoryId;
        Price = productPrice;
        Image = image;
    }

    public void RemoveStock(int quantityDesired)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(0, quantityDesired);
        Quantity -= quantityDesired;
        if (Status == ProductStatus.InStock && Quantity == 0) Status = ProductStatus.OutOfStock;
    }

    public void AddStock(int quantityDesired)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(0, quantityDesired);
        Quantity += quantityDesired;
        if (Status == ProductStatus.OutOfStock && Quantity > 0) Status = ProductStatus.InStock;
    }

    public void SetDiscontinued() => Status = ProductStatus.Discontinued;

    public void DisableProduct(ProductId productId) => RegisterDomainEvent(new ProductDisabledEvent(productId));
}