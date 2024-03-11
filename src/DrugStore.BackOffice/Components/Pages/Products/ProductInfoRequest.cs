namespace DrugStore.BackOffice.Components.Pages.Products;

public sealed class ProductInfoRequest
{
    public string? Name { get; set; }
    public string? ProductCode { get; set; }
    public string? Detail { get; set; }
    public int Quantity { get; set; }
    public Guid? CategoryId { get; set; }
    public ProductPriceRequest ProductPrice { get; set; } = new();
}