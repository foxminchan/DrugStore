using Refit;

namespace DrugStore.BackOffice.Components.Pages.Categories.Requests;

public sealed class UpdateCategory : CreateCategory
{
    [AliasAs("id")]
    public string? Id { get; set; }
}