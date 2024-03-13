using DrugStore.Domain.ProductAggregate.Primitives;

namespace DrugStore.WebAPI.Endpoints.Product;

public sealed class UpdateProductImageRequest(ProductId id, IFormFile image, string alt)
{
    public ProductId Id { get; set; } = id;
    public IFormFile Image { get; set; } = image;
    public string Alt { get; set; } = alt;
}