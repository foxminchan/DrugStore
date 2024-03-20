namespace DrugStore.WebAPI.Endpoints.Category;

public sealed class UpdateCategoryResponse(CategoryDto category)
{
    public CategoryDto Category { get; set; } = category;
}