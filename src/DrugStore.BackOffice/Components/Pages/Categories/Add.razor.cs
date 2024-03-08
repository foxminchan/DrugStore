using Ardalis.Result;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace DrugStore.BackOffice.Components.Pages.Categories;

public partial class Add
{
    [Inject] private ICategoriesApi CategoriesApi { get; set; } = default!;

    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    [Inject] private NotificationService NotificationService { get; set; } = default!;

    private readonly List<string> _errorMessages = [];

    private readonly CategoryCreateRequest _category = new();

    private async Task OnSubmit(CategoryCreateRequest category)
    {
        var result = await CategoriesApi.AddCategoryAsync(category, Guid.NewGuid().ToString());

        switch (result.Status)
        {
            case ResultStatus.Ok:
                NotificationService.Notify(new()
                {
                    Severity = NotificationSeverity.Success,
                    Summary = "Success",
                    Detail = "Category added successfully!"
                });
                NavigationManager.NavigateTo("/categories");
                break;
            case ResultStatus.Invalid:
            {
                foreach (var error in result.ValidationErrors) 
                    _errorMessages.Add(error.ErrorMessage);
                break;
            }
            default:
                _errorMessages.Add("An error occurred while adding the category. Please try again.");
                break;
        }
    }

    private static bool ValidateCategoryName(string? name) => !string.IsNullOrEmpty(name) && name.Length <= 50;

    private static bool ValidateCategoryDescription(string? description)
    {
        if (string.IsNullOrEmpty(description)) return true;

        return description.Length <= 100;
    }
}