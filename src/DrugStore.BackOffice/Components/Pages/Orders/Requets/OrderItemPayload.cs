using Refit;

namespace DrugStore.BackOffice.Components.Pages.Orders.Requets;

public sealed class OrderItemPayload
{
    [AliasAs("id")] public Guid Id { get; set; }

    [AliasAs("quantity")] public int Quantity { get; set; }

    [AliasAs("price")] public decimal Price { get; set; }
}