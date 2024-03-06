using Microsoft.AspNetCore.Components;

namespace DrugStore.BackOffice.Components.Pages.Categories;

public partial class Edit
{
    [Inject] private ICategoriesApi CategoriesApi { get; set; } = default!;

    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    private readonly CategoryUpdateRequest _category = new();

    [Parameter] public required string Id { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var category = await CategoriesApi.GetCategoryAsync(new(Id));
        if (category.IsSuccess)
        {
            _category.Id = category.Value.Id.ToString();
            _category.Name = category.Value.Name;
            _category.Description = category.Value.Description;
        }
    }

    private async Task UpdateCategory(CategoryUpdateRequest category)
    {
        var result = await CategoriesApi.UpdateCategoryAsync(category);
        if (result.IsSuccess)
        {
            NavigationManager.NavigateTo("/categories");
        }
    }
}