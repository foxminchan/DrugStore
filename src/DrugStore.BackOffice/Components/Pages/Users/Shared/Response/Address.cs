using System.Text.Json.Serialization;

namespace DrugStore.BackOffice.Components.Pages.Users.Shared.Response;

public sealed class Address
{
    [JsonPropertyName("street")] public string? Street { get; set; }

    [JsonPropertyName("city")] public string? City { get; set; }

    [JsonPropertyName("province")] public string? Province { get; set; }
}