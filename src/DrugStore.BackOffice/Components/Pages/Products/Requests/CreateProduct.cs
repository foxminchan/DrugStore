using Refit;

namespace DrugStore.BackOffice.Components.Pages.Products.Requests;

public class CreateProduct
{
    [AliasAs("name")] public string? Name { get; set; }

    [AliasAs("productCode")] public string? ProductCode { get; set; }

    [AliasAs("detail")] public string? Detail { get; set; }

    [AliasAs("quantity")] public int Quantity { get; set; }

    [AliasAs("categoryId")] public Guid? CategoryId { get; set; }

    [AliasAs("price")] public decimal Price { get; set; } = 0;

    [AliasAs("priceSale")] public decimal PriceSale { get; set; } = 0;

    [AliasAs("image")] public IFormFile? File { get; set; }

    [AliasAs("alt")] public string? Alt { get; set; }
}