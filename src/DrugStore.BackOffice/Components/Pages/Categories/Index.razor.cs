using DrugStore.BackOffice.Components.Pages.Categories.Responses;
using DrugStore.BackOffice.Components.Pages.Categories.Services;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace DrugStore.BackOffice.Components.Pages.Categories;

public sealed partial class Index
{
    [Inject] private ICategoriesApi CategoriesApi { get; set; } = default!;

    [Inject] private DialogService DialogService { get; set; } = default!;

    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    [Inject] private NotificationService NotificationService { get; set; } = default!;

    private RadzenDataGrid<Category> _dataGrid = default!;
    private List<Category> _categories = [];
    private bool _loading;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            _loading = true;
            var result = await CategoriesApi.GetCategoriesAsync();
            _categories = result.Categories;
        }
        catch (Exception)
        {
            NotificationService.Notify(new()
                { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Unable to load Category" });
        }
        finally
        {
            _loading = false;
        }
    }

    private async Task EditCategory(Guid id)
    {
        NavigationManager.NavigateTo($"/categories/edit/{id}");
        await Task.CompletedTask;
    }

    private async Task AddCategory()
    {
        NavigationManager.NavigateTo("/categories/add");
        await Task.CompletedTask;
    }

    private async Task DeleteCategory(Guid id)
    {
        var result = await DialogService.Confirm(
            message: "Are you sure?",
            title: "Delete Category",
            options: new()
            {
                OkButtonText = "Yes",
                CancelButtonText = "No"
            }
        ) ?? false;

        if (!result)
            return;

        try
        {
            await CategoriesApi.DeleteCategoryAsync(id);
            NotificationService.Notify(new()
            {
                Severity = NotificationSeverity.Success,
                Summary = "Success",
                Detail = "Category deleted successfully!"
            });
            await _dataGrid.Reload();
        }
        catch (Exception)
        {
            NotificationService.Notify(new()
            {
                Severity = NotificationSeverity.Error,
                Summary = "Error",
                Detail = "Unable to delete Category"
            });
        }
    }
}