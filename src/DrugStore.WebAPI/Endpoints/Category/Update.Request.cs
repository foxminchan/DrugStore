using DrugStore.Domain.CategoryAggregate.Primitives;

namespace DrugStore.WebAPI.Endpoints.Category;

public sealed class UpdateCategoryRequest
{
    public CategoryId Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}