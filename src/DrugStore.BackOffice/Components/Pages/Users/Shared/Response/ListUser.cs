using System.Text.Json.Serialization;
using Ardalis.Result;

namespace DrugStore.BackOffice.Components.Pages.Users.Shared.Response;

public sealed class ListUser
{
    [JsonPropertyName("pagedInfo")] public PagedInfo PagedInfo { get; set; } = default!;

    [JsonPropertyName("users")] public List<User> Users { get; set; } = [];
}