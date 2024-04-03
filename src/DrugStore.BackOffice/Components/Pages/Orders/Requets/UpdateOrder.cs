using Refit;

namespace DrugStore.BackOffice.Components.Pages.Orders.Requets;

public sealed class UpdateOrder : CreateOrder
{
    [AliasAs("id")] public string? Id { get; set; }
}