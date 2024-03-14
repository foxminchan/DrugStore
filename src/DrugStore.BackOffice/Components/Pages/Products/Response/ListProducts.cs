using System.Text.Json.Serialization;
using Ardalis.Result;

namespace DrugStore.BackOffice.Components.Pages.Products.Response;

public sealed class ListProducts
{
    [JsonPropertyName("pagedInfo")] public PagedInfo PagedInfo { get; set; } = default!;

    [JsonPropertyName("products")] public List<Product> Products { get; set; } = [];
}