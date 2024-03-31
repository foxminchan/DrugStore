using Refit;

namespace DrugStore.BackOffice.Components.Pages.Orders.Requets;

public class CreateOrder
{
    [AliasAs("code")] public string? Code { get; set; }

    [AliasAs("customerId")] public Guid? CustomerId { get; set; }

    [AliasAs("items")] public List<OrderItemPayload> Items { get; set; } = [];
}