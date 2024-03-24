using System.Text.Json.Serialization;

namespace DrugStore.BackOffice.Components.Pages.Categories.Responses;

public sealed class ListCategories
{
    [JsonPropertyName("categories")] public List<Category> Categories { get; set; } = [];
}