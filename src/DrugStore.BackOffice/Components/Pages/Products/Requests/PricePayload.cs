using Refit;

namespace DrugStore.BackOffice.Components.Pages.Products.Requests;

public sealed class PricePayload
{
    [AliasAs("price")] public decimal Price { get; set; }

    [AliasAs("priceSale")] public decimal PriceSale { get; set; }
}