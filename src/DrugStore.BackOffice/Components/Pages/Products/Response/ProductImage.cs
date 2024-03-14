using System.Text.Json.Serialization;

namespace DrugStore.BackOffice.Components.Pages.Products.Response;

public sealed class ProductImage
{
    [JsonPropertyName("imageUrl")] public string? ImageUrl { get; set; }

    [JsonPropertyName("alt")] public string? Alt { get; set; }

    [JsonPropertyName("title")] public string? Title { get; set; }
}