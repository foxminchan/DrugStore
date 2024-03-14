using System.Text.Json.Serialization;

namespace DrugStore.BackOffice.Components.Pages.Orders.Response;

public sealed class OrderDetail
{
    [JsonPropertyName("order")] public Order Order { get; set; } = new();

    [JsonPropertyName("items")] public List<OrderItem> Items { get; set; } = [];
}