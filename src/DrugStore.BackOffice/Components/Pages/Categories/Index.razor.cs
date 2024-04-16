using DrugStore.BackOffice.Components.Pages.Categories.Responses;
using DrugStore.BackOffice.Components.Pages.Categories.Services;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace DrugStore.BackOffice.Components.Pages.Categories;

public sealed partial class Index
{
    private List<Category> _categories = [];

    private RadzenDataGrid<Category> _dataGrid = default!;
    private bool _error;

    private bool _loading;

    [Inject] private ICategoriesApi CategoriesApi { get; set; } = default!;

    [Inject] private DialogService DialogService { get; set; } = default!;

    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    [Inject] private NotificationService NotificationService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            _error = false;
            _loading = true;
            var result = await CategoriesApi.ListCategoriesAsync();
            _categories = result.Categories;
        }
        catch (Exception)
        {
            NotificationService.Notify(new()
                { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Unable to load Category" });
            _error = true;
        }
        finally
        {
            _loading = false;
        }
    }

    private async Task EditCategory(Guid id)
    {
        await DialogService.OpenAsync<Edit>("Edit Category", new() { { "Id", id } });
        await _dataGrid.Reload();
    }

    private async Task AddCategory()
    {
        await DialogService.OpenAsync<Add>("Add Category");
        await _dataGrid.Reload();
    }

    private async Task DeleteCategory(Guid id)
    {
        try
        {
            if (await DialogService.Confirm("Are you sure you want to delete this category?") == true)
            {
                await CategoriesApi.DeleteCategoryAsync(id);
                await _dataGrid.Reload();
                NotificationService.Notify(new()
                {
                    Severity = NotificationSeverity.Success,
                    Summary = "Success",
                    Detail = "Category deleted successfully!"
                });
            }
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

    private async Task ExportClick() => NavigationManager.NavigateTo("/export/categories", true);
}