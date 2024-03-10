using Microsoft.AspNetCore.Components;

namespace DrugStore.BackOffice.Components.Pages.Products;

public partial class Edit
{
    [Parameter] public required string Id { get; set; }
}