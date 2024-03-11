namespace DrugStore.BackOffice.Components.Pages.Products;

public sealed class ProductImageRequest
{
    public IFormFile? File { get; set; }
    public string? Alt { get; set; }
}