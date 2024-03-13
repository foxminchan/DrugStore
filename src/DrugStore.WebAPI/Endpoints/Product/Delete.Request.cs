using DrugStore.Domain.ProductAggregate.Primitives;

namespace DrugStore.WebAPI.Endpoints.Product;

public sealed class DeleteProductRequest(ProductId id)
{
    public ProductId Id { get; set; } = id;
}