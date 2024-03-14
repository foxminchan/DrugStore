using System.Text.Json.Serialization;

namespace DrugStore.BackOffice.Components.Pages.Orders.Response;

public sealed class OrderItem
{
    [JsonPropertyName("productId")] public Guid ProductId { get; set; }

    [JsonPropertyName("productName")] public Guid OrderId { get; set; }

    [JsonPropertyName("quantity")] public int Quantity { get; set; }

    [JsonPropertyName("price")] public decimal Price { get; set; }

    [JsonPropertyName("total")] public decimal Total { get; set; }
}