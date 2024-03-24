using Refit;

namespace DrugStore.BackOffice.Components.Pages.Categories.Requests;

public class CreateCategory
{
    [AliasAs("name")] public string? Name { get; set; }

    [AliasAs("description")] public string? Description { get; set; }
}