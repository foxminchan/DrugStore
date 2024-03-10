using Ardalis.Result;
using DrugStore.BackOffice.Services;
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

    [Inject] private ExportService<CategoryResponse> ExportService { get; set; } = default!;

    private RadzenDataGrid<CategoryResponse> _dataGrid = default!;
    private readonly List<string> _errors = [];
    private List<CategoryResponse> _categories = [];
    private bool _loading;

    protected override async Task OnInitializedAsync()
    {
        _loading = true;
        var result = await CategoriesApi.GetCategoriesAsync();
        _loading = false;
        if (result.Status == ResultStatus.Ok)
            _categories = result.Value;
        else
            _errors.Add("An error occurred while retrieving categories. Please try again.");
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
            "Are you sure?",
            "Delete Category",
            new() { OkButtonText = "Yes", CancelButtonText = "No" }
        ) ?? false;
        if (!result) return;
        var status = await CategoriesApi.DeleteCategoryAsync(id);

        if (status.Status != ResultStatus.Ok)
        {
            NotificationService.Notify(new()
            {
                Severity = NotificationSeverity.Error,
                Summary = "Error",
                Detail = "An error occurred while deleting the category. Please try again."
            });

            return;
        }

        NotificationService.Notify(new()
        {
            Severity = NotificationSeverity.Success,
            Summary = "Success",
            Detail = "Category deleted successfully!"
        });
        _categories.RemoveAll(category => category.Id == id);
        await _dataGrid.Reload();
    }

    private Task ExportToCsv()
    {
        ExportService.ExportCsv(_categories.AsQueryable());
        return Task.CompletedTask;
    }
}