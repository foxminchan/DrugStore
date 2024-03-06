using Ardalis.Result;
using Refit;

namespace DrugStore.BackOffice.Components.Pages.Categories;

public interface ICategoriesApi
{
    [Get("/categories")]
    Task<Result<List<CategoryResponse>>> GetCategoriesAsync();

    [Get("/categories/{id}")]
    Task<Result<CategoryResponse>> GetCategoryAsync(Guid id);

    [Delete("/categories/{id}")]
    Task<Result> DeleteCategoryAsync(Guid id);

    [Post("/categories")]
    Task<Result<Guid>> AddCategoryAsync(CategoryCreateRequest category, [Header("X-Idempotency-Key")] string key);

    [Put("/categories")]
    Task<Result> UpdateCategoryAsync(CategoryUpdateRequest category);
}