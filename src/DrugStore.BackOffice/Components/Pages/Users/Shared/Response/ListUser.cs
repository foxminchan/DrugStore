using Ardalis.Result;
using System.Text.Json.Serialization;

namespace DrugStore.BackOffice.Components.Pages.Users.Shared.Response;

public sealed class ListUser
{
    [JsonPropertyName("pagedInfo")] public PagedInfo? PagedInfo { get; set; }

    [JsonPropertyName("users")] public List<User>? Users { get; set; }
}