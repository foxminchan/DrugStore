using System.Text.Json.Serialization;

namespace DrugStore.BackOffice.Components.Pages.Products;

public sealed class ProductResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("productCode")]
    public string? ProductCode { get; set; }

    [JsonPropertyName("detail")]
    public string? Detail { get; set; }

    [JsonPropertyName("status")]
    public string? Status { get; set; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    [JsonPropertyName("category")]
    public string? Category { get; set; }

    [JsonPropertyName("price")]
    public ProductPrice? Price { get; set; }

    [JsonPropertyName("image")]
    public ProductImage? Image { get; set; }
}

public sealed class ProductPrice
{
    [JsonPropertyName("price")]
    public decimal Price { get; set; }

    [JsonPropertyName("priceSale")]
    public decimal PriceSale { get; set; }
}

public sealed class ProductImage
{
    [JsonPropertyName("imageUrl")]
    public string? ImageUrl { get; set; }

    [JsonPropertyName("alt")]
    public string? Alt { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }
}