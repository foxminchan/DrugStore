using Refit;

namespace DrugStore.BackOffice.Components.Pages.Products.Requests;

public class ProductPayload
{
    [AliasAs("name")] public string? Name { get; set; }

    [AliasAs("productCode")] public string? ProductCode { get; set; }

    [AliasAs("detail")] public string? Detail { get; set; }

    [AliasAs("quantity")] public int Quantity { get; set; }

    [AliasAs("categoryId")] public Guid? CategoryId { get; set; }

    [AliasAs("productPrice")] public PricePayload ProductPrice { get; set; } = new();
}