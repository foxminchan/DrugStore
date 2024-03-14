using System.Text.Json.Serialization;
using Ardalis.Result;

namespace DrugStore.BackOffice.Components.Pages.Orders.Response;

public sealed class ListOrderDetail
{
    [JsonPropertyName("pagedInfo")] public PagedInfo PagedInfo { get; set; } = default!;

    [JsonPropertyName("orders")] public List<OrderDetail> Orders { get; set; } = [];
}