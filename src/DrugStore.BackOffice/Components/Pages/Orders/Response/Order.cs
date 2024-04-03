using System.Text.Json.Serialization;

namespace DrugStore.BackOffice.Components.Pages.Orders.Response;

public sealed class Order
{
    [JsonPropertyName("id")] public Guid Id { get; set; }

    [JsonPropertyName("code")] public string? Code { get; set; }

    [JsonPropertyName("customerName")] public string? CustomerName { get; set; }

    [JsonPropertyName("customerId")] public Guid CustomerId { get; set; }

    [JsonPropertyName("total")] public decimal Total { get; set; }
}