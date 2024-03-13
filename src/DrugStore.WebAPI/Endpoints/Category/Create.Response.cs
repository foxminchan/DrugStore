using DrugStore.Domain.CategoryAggregate.Primitives;

namespace DrugStore.WebAPI.Endpoints.Category;

public sealed class CreateCategoryResponse(CategoryId id)
{
    public CategoryId Id { get; set; } = id;
}