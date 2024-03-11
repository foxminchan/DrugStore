using Ardalis.Result;
using DrugStore.BackOffice.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;

namespace DrugStore.BackOffice.Components.Pages.Categories;

public sealed partial class Add
{
    [Inject] private ICategoriesApi CategoriesApi { get; set; } = default!;

    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    [Inject] private NotificationService NotificationService { get; set; } = default!;

    private bool _busy;

    private readonly List<string> _errors = [];

    private readonly CategoryCreateRequest _category = new();

    private async Task OnSubmit(CategoryCreateRequest category)
    {
        _busy = true;
        var result = await CategoriesApi.AddCategoryAsync(category, Guid.NewGuid().ToString());
        _busy = false;

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
                    _errors.Add(error.ErrorMessage);
                break;
            }
            default:
                _errors.Add("An error occurred while adding the category. Please try again.");
                break;
        }
    }

    private static bool ValidateCategoryName(string? name)
        => !string.IsNullOrEmpty(name) && name.Length <= DataLengthHelper.ShortLength;

    private static bool ValidateCategoryDescription(string? description)
    {
        if (string.IsNullOrEmpty(description)) return true;

        return description.Length <= DataLengthHelper.LongLength;
    }

    private async Task CancelButtonClick(MouseEventArgs arg)
    {
        NavigationManager.NavigateTo("/categories");
        await Task.CompletedTask;
    }
}