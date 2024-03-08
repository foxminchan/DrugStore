namespace DrugStore.BackOffice.Components.Pages.Products;

public class ProductResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? ProductCode { get; set; }
    public string? Detail { get; set; }
    //public     ProductStatus Status,
    public int Quantity { get; set; }
}