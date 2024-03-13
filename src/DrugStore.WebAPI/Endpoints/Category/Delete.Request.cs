using DrugStore.Domain.CategoryAggregate.Primitives;

namespace DrugStore.WebAPI.Endpoints.Category;

public sealed class DeleteCategoryRequest(CategoryId id)
{
    public CategoryId Id { get; set; } = id;
}