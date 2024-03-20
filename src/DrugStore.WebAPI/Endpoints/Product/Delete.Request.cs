using DrugStore.Domain.ProductAggregate.Primitives;

namespace DrugStore.WebAPI.Endpoints.Product;

public sealed class DeleteProductRequest(ProductId id, bool isRemoveImage)
{
    public ProductId Id { get; set; } = id;
    public bool IsRemoveImage { get; set; } = isRemoveImage;
}