using DrugStore.Domain.ProductAggregate.Primitives;

namespace DrugStore.WebAPI.Endpoints.Product;

public sealed class GetProductByIdRequest(ProductId id)
{
    public ProductId Id { get; set; } = id;
}