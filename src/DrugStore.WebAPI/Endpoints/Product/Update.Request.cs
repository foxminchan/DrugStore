using DrugStore.Domain.CategoryAggregate.Primitives;
using DrugStore.Domain.ProductAggregate.Primitives;
using DrugStore.Domain.ProductAggregate.ValueObjects;

namespace DrugStore.WebAPI.Endpoints.Product;

public sealed class UpdateProductRequest
{
    public ProductId Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? ProductCode { get; set; }
    public string? Detail { get; set; }
    public int Quantity { get; set; }
    public CategoryId? CategoryId { get; set; }
    public ProductPrice ProductPrice { get; set; } = new();
    public string? ImageUrl { get; set; }
}