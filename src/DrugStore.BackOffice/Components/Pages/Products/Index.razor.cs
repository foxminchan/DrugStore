using DrugStore.BackOffice.Components.Pages.Products.Response;
using DrugStore.BackOffice.Components.Pages.Products.Services;
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

    private RadzenDataGrid<Product> _dataGrid = default!;
    private List<Product> _products = [];
    private bool _loading;
    private int _count;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            _loading = true;
            var result = await ProductsApi.GetProductsAsync(new());
            _products = result.Products;
            _count = (int)result.PagedInfo.TotalRecords;
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

        if (!result)
            return;

        try
        {
            await ProductsApi.DeleteProductAsync(id);

            NotificationService.Notify(new()
            {
                Severity = NotificationSeverity.Error,
                Summary = "Error",
                Detail = "An error occurred while deleting the product. Please try again."
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

    private async Task Search(ChangeEventArgs args)
    {
        try
        {
            _loading = true;
            var result = await ProductsApi.GetProductsAsync(new() { Search = args.Value?.ToString() });
            _products = result.Products;
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
}