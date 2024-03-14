using DrugStore.BackOffice.Components.Pages.Categories.Requests;
using DrugStore.BackOffice.Components.Pages.Categories.Responses;
using Refit;

namespace DrugStore.BackOffice.Components.Pages.Categories.Services;

public interface ICategoriesApi
{
    [Get("/categories")]
    Task<ListCategories> GetCategoriesAsync();

    [Get("/categories/{id}")]
    Task<Category> GetCategoryAsync(Guid id);

    [Delete("/categories/{id}")]
    Task DeleteCategoryAsync(Guid id);

    [Post("/categories")]
    Task<Guid> AddCategoryAsync(CreateCategory category, [Header("X-Idempotency-Key")] Guid key);

    [Put("/categories")]
    Task UpdateCategoryAsync(UpdateCategory category);
}