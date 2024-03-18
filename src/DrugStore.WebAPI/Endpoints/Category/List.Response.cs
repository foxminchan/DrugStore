namespace DrugStore.WebAPI.Endpoints.Category;

public sealed class ListCategoryResponse(List<CategoryDto> categories)
{
    public List<CategoryDto> Categories { get; set; } = categories;
}