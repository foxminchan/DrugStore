using Ardalis.Result;
using DrugStore.BackOffice.Services;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace DrugStore.BackOffice.Components.Pages.Products;

public sealed partial class Index
{
    [Inject] private IProductsApi ProductsApi { get; set; } = default!;

    [Inject] private DialogService DialogService { get; set; } = default!;

    [Inject] private NotificationService NotificationService { get; set; } = default!;

    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    [Inject] private ExportService<ProductResponse> ExportService { get; set; } = default!;

    private RadzenDataGrid<ProductResponse> _dataGrid = default!;
    private readonly List<string> _errors = [];
    private List<ProductResponse> _products = [];
    private bool _loading;
    private int _count;

    protected override async Task OnInitializedAsync()
    {
        _loading = true;
        var result = await ProductsApi.GetProductsAsync(new());
        _loading = false;

        if (result.Status == ResultStatus.Ok)
        {
            _products = result.Value;
            _count = (int)result.PagedInfo.TotalRecords;
        }
        else
        {
            _errors.Add("An error occurred while retrieving products. Please try again.");
        }
    }

    private async Task AddProducts()
    {
        NavigationManager.NavigateTo("/products/add");
        await Task.CompletedTask;
    }

    private async Task EditProduct(Guid id)
    {
        NavigationManager.NavigateTo($"/products/edit/{id}");
        await Task.CompletedTask;
    }

    private async Task DeleteProduct(Guid id)
    {
        var result = await DialogService.Confirm(
            "Are you sure?",
            "Delete Product",
            new() { OkButtonText = "Yes", CancelButtonText = "No" }
        ) ?? false;
        if (!result) return;
        var status = await ProductsApi.DeleteProductAsync(id);

        if (status.Status != ResultStatus.Ok)
        {
            NotificationService.Notify(new()
            {
                Severity = NotificationSeverity.Error,
                Summary = "Error",
                Detail = "An error occurred while deleting the product. Please try again."
            });

            return;
        }

        NotificationService.Notify(new()
        {
            Severity = NotificationSeverity.Success,
            Summary = "Success",
            Detail = "Product deleted successfully."
        });
        _products.RemoveAll(p => p.Id == id);
        _count--;
        await _dataGrid.Reload();
    }

    private async Task ExportToCsv()
    {
        ExportService.ExportCsv(_products.AsQueryable());
        await Task.CompletedTask;
    }

    private async Task Search(ChangeEventArgs args)
    {
        _loading = true;
        var result = await ProductsApi.GetProductsAsync(new() { Search = args.Value?.ToString() });
        _loading = false;

        if (result.Status == ResultStatus.Ok)
        {
            _products = result.Value;
            _count = (int)result.PagedInfo.TotalRecords;
        }
        else
        {
            _errors.Add("An error occurred while retrieving products. Please try again.");
        }
    }
}