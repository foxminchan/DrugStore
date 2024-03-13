using DrugStore.Domain.CategoryAggregate.Primitives;
using DrugStore.Domain.ProductAggregate.ValueObjects;

namespace DrugStore.WebAPI.Endpoints.Product;

public sealed class CreateProductRequest(string idempotency, CreateProductPayload product)
{
    public string Idempotency { get; set; } = idempotency;
    public CreateProductPayload Product { get; set; } = product;
}

public sealed class CreateProductPayload
{
    public string Name { get; set; } = string.Empty;
    public string? ProductCode { get; set; }
    public string? Detail { get; set; }
    public int Quantity { get; set; } = 0;
    public CategoryId? CategoryId { get; set; }
    public ProductPrice ProductPrice { get; set; } = new(0, 0);
}