using DrugStore.BackOffice.Components.Pages.Categories.Requests;
using DrugStore.BackOffice.Components.Pages.Categories.Services;
using DrugStore.BackOffice.Constants;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;

namespace DrugStore.BackOffice.Components.Pages.Categories;

public sealed partial class Add
{
    [Inject] private ICategoriesApi CategoriesApi { get; set; } = default!;

    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    [Inject] private NotificationService NotificationService { get; set; } = default!;

    [Inject] private DialogService DialogService { get; set; } = default!;

    private bool _busy;

    private bool _error;

    private readonly CreateCategory _category = new();

    private async Task OnSubmit(CreateCategory category)
    {
        try
        {
            _busy = true;
            await CategoriesApi.AddCategoryAsync(category, Guid.NewGuid());
            NotificationService.Notify(new()
            {
                Severity = NotificationSeverity.Success,
                Summary = "Success",
                Detail = "Category added successfully!"
            });
            NavigationManager.NavigateTo("/categories");
        }
        catch (Exception)
        {
            NotificationService.Notify(new()
                { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Unable to add Category" });
            _error = true;
        }
        finally
        {
            _busy = false;
        }
    }

    private static bool ValidateCategoryName(string? name)
        => !string.IsNullOrEmpty(name) && name.Length <= DataTypeLength.ShortLength;

    private static bool ValidateCategoryDescription(string? description)
    {
        if (string.IsNullOrEmpty(description)) return true;
        return description.Length <= DataTypeLength.LongLength;
    }

    private async Task CancelButtonClick(MouseEventArgs arg) => DialogService.Close();
}