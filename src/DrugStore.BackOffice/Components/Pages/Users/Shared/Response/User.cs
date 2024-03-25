using System.Text.Json.Serialization;

namespace DrugStore.BackOffice.Components.Pages.Users.Shared.Response;

public sealed class User
{
    [JsonPropertyName("id")] public Guid Id { get; set; }

    [JsonPropertyName("email")] public string? Email { get; set; }

    [JsonPropertyName("fullName")] public string? FullName { get; set; }

    [JsonPropertyName("phone")] public string? Phone { get; set; }

    [JsonPropertyName("address")] public Address? Address { get; set; }
}