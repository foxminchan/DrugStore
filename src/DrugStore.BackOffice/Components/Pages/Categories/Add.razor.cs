using Ardalis.Result;
using Microsoft.AspNetCore.Components;

namespace DrugStore.BackOffice.Components.Pages.Categories;

public partial class Add
{
    [Inject] private ICategoriesApi CategoriesApi { get; set; } = default!;

    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    private readonly CategoryCreateRequest _category = new();

    private async Task AddCategory(CategoryCreateRequest category)
    { 
        var result = await CategoriesApi.AddCategoryAsync(category, Guid.NewGuid().ToString());
        if (result.Status == ResultStatus.Ok)
        {
            NavigationManager.NavigateTo("/categories");
        }
    }
}