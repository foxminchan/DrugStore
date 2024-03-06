using Ardalis.Result;
using Refit;

namespace DrugStore.BackOffice.Components.Pages.Categories;

public interface ICategoriesApi
{
    [Get("/categories")]
    Task<Result<List<CategoriesResponse>>> GetCategoriesAsync();

    [Delete("/categories/{id}")]
    Task<Result> DeleteCategoryAsync(Guid id);
}