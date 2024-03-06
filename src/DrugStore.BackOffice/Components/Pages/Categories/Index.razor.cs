using DrugStore.BackOffice.Services;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace DrugStore.BackOffice.Components.Pages.Categories;

public sealed partial class Index
{
    [Inject] private ICategoriesApi CategoriesApi { get; set; } = default!;

    [Inject] private DialogService DialogService { get; set; } = default!;

    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    [Inject] private NotificationService NotificationService { get; set; } = default!;

    [Inject] private ExportService<CategoryResponse> ExportService { get; set; } = default!;

    private List<CategoryResponse> _categories = [];
    private bool _loading;

    protected override async Task OnInitializedAsync()
    {
        _loading = true;
        _categories = await CategoriesApi.GetCategoriesAsync();
        _loading = false;
    }

    private async Task EditCategory(Guid id)
        => await DialogService.OpenAsync<Edit>("Edit Category",
            new() { ["Id"] = id },
            new() { Width = "40%", Height = "60%" }
        );

    private async Task AddCategory()
        => await DialogService.OpenAsync<Add>("Add Category", options: new() { Width = "40%", Height = "60%" });

    private async Task DeleteCategory(Guid id)
    {
        var result = await DialogService.Confirm(
            "Are you sure?",
            "Delete Category",
            new() { OkButtonText = "Yes", CancelButtonText = "No" }
        ) ?? false;
        if (!result) return;
        await CategoriesApi.DeleteCategoryAsync(id);
        NotificationService.Notify(new()
        {
            Severity = NotificationSeverity.Success,
            Summary = "Success",
            Detail = "Category deleted successfully!"
        });
        _categories.RemoveAll(category => category.Id == id);
        StateHasChanged();
    }

    private Task ExportToCsv()
    {
        var result = ExportService.ExportCsv(_categories.AsQueryable());
        NavigationManager.NavigateTo(result.FileDownloadName);
        return Task.CompletedTask;
    }
}