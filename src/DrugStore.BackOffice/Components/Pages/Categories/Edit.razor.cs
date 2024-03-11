using Ardalis.Result;
using DrugStore.BackOffice.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;

namespace DrugStore.BackOffice.Components.Pages.Categories;

public sealed partial class Edit
{
    [Inject] private ICategoriesApi CategoriesApi { get; set; } = default!;

    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    [Inject] private NotificationService NotificationService { get; set; } = default!;

    private bool _busy;

    private bool _loading;

    private readonly List<string> _errors = [];

    private readonly CategoryUpdateRequest _category = new();

    [Parameter] public required string Id { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _loading = true;
        var category = await CategoriesApi.GetCategoryAsync(new(Id));
        _loading = false;

        if (category.IsSuccess)
        {
            _category.Id = category.Value.Id.ToString();
            _category.Name = category.Value.Name;
            _category.Description = category.Value.Description;
        }
        else
        {
            NotificationService.Notify(new()
            {
                Severity = NotificationSeverity.Error,
                Summary = "Error",
                Detail = "Category not found!"
            });
            NavigationManager.NavigateTo("/categories");
        }
    }

    private async Task OnSubmit(CategoryUpdateRequest category)
    {
        _busy = true;
        var result = await CategoriesApi.UpdateCategoryAsync(category);
        _busy = false;

        switch (result.Status)
        {
            case ResultStatus.Ok:
                NotificationService.Notify(new()
                {
                    Severity = NotificationSeverity.Success,
                    Summary = "Success",
                    Detail = "Category updated successfully!"
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