namespace DrugStore.BackOffice.Components.Pages.Products.Requests;

public sealed class CreateProduct
{
    public ProductPayload Product { get; set; } = new();
    public ImagePayload Image { get; set; } = new();
}