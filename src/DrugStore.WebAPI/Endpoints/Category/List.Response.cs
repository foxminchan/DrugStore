namespace DrugStore.WebAPI.Endpoints.Category;

public sealed class ListCategoryResponse
{
    public List<CategoryDto>? Categories { get; set; } = [];
}