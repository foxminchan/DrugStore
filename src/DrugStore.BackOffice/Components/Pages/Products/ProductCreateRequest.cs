namespace DrugStore.BackOffice.Components.Pages.Products;

public sealed class ProductCreateRequest
{
    public ProductInfoRequest Product { get; set; } = new();
    public ProductImageRequest? Image { get; set; } = new();
}