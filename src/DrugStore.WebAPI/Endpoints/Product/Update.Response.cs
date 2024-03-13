namespace DrugStore.WebAPI.Endpoints.Product;

public sealed class UpdateProductResponse(ProductDto product)
{
    public ProductDto Product { get; set; } = product;
}