using System.Text.Json.Serialization;

namespace DrugStore.BackOffice.Components.Pages.Products.Response;

public sealed class ProductPrice
{
    [JsonPropertyName("price")] public decimal Price { get; set; }

    [JsonPropertyName("priceSale")] public decimal PriceSale { get; set; }
}