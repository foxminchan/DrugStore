using DrugStore.BackOffice.Components.Pages.Categories.Requests;
using DrugStore.BackOffice.Components.Pages.Categories.Services;
using DrugStore.BackOffice.Constants;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;

namespace DrugStore.BackOffice.Components.Pages.Categories;

public sealed partial class Edit
{
    private readonly UpdateCategory _category = new();

    private bool _busy;

    private bool _error;
    [Inject] private ICategoriesApi CategoriesApi { get; set; } = default!;

    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    [Inject] private NotificationService NotificationService { get; set; } = default!;

    [Inject] private DialogService DialogService { get; set; } = default!;

    [Parameter] public required Guid Id { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            _busy = true;
            _error = false;
            var category = await CategoriesApi.GetCategoryAsync(Id);
            _category.Id = category.Id.ToString();
            _category.Name = category.Name;
            _category.Description = category.Description;
        }
        catch (Exception)
        {
            NotificationService.Notify(new()
                { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Unable to load Category" });
            NavigationManager.NavigateTo("/categories");
        }
        finally
        {
            _busy = false;
        }
    }

    private async Task OnSubmit(UpdateCategory category)
    {
        try
        {
            _busy = true;
            await CategoriesApi.UpdateCategoryAsync(category);
            NotificationService.Notify(new()
            {
                Severity = NotificationSeverity.Success,
                Summary = "Success",
                Detail = "Category updated successfully!"
            });
            NavigationManager.NavigateTo("/categories");
        }
        catch (Exception)
        {
            NotificationService.Notify(new()
                { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Unable to add Category" }
            );
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