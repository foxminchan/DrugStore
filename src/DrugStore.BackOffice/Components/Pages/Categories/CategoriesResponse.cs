using System.Text.Json.Serialization;

namespace DrugStore.BackOffice.Components.Pages.Categories;

public sealed class CategoriesResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }
}
