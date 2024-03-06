namespace DrugStore.BackOffice.Components.Pages.Categories;

public sealed class CategoryCreateRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}

public sealed class CategoryUpdateRequest
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}