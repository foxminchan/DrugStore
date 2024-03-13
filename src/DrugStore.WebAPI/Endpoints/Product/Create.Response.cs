using DrugStore.Domain.ProductAggregate.Primitives;

namespace DrugStore.WebAPI.Endpoints.Product;

public sealed class CreateProductResponse(ProductId id)
{
    public ProductId Id { get; set; } = id;
}