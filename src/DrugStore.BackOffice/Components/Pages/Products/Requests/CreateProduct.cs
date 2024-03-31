namespace DrugStore.BackOffice.Components.Pages.Products.Requests;

public class CreateProduct
{
    public string? Name { get; set; }

    public string? ProductCode { get; set; }

    public string? Detail { get; set; }

    public int Quantity { get; set; }

    public Guid? CategoryId { get; set; }

    public decimal Price { get; set; } = 0;

    public decimal PriceSale { get; set; } = 0;

    public IFormFile? File { get; set; }

    public string? Alt { get; set; }
}