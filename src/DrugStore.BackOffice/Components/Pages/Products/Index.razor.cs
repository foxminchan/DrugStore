using DrugStore.BackOffice.Components.Pages.Products.Response;
using DrugStore.BackOffice.Components.Pages.Products.Services;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace DrugStore.BackOffice.Components.Pages.Products;

public sealed partial class Index
{
    private int _count;

    private bool _error;

    private bool _loading;

    private List<Product> _products = [];

    private RadzenDataGrid<Product> _dataGrid = default!;

    [Inject] private IProductsApi ProductsApi { get; set; } = default!;

    [Inject] private DialogService DialogService { get; set; } = default!;

    [Inject] private NotificationService NotificationService { get; set; } = default!;

    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            _error = false;
            _loading = true;
            var result = await ProductsApi.ListProductsAsync(new());
            _products = result.Products;
            _count = (int)result.PagedInfo.TotalRecords;
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

    private async Task DeleteProduct(Guid id, string? image)
    {
        try
        {
            if (await DialogService.Confirm("Are you sure you want to delete this product?") == true)
            {
                var deleteImage = false;

                if (!string.IsNullOrEmpty(image))
                    deleteImage = await DialogService.Confirm("Do you want to delete the image?") ?? false;

                await ProductsApi.DeleteProductAsync(id, deleteImage);
                _products.RemoveAll(x => x.Id == id);
                _count = _products.Count;
                await _dataGrid.Reload();
                NotificationService.Notify(new()
                {
                    Severity = NotificationSeverity.Success,
                    Summary = "Success",
                    Detail = "Product deleted successfully."
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

    private async Task Search(ChangeEventArgs args)
    {
        try
        {
            _loading = true;
            var result = await ProductsApi.ListProductsAsync(new() { Search = args.Value?.ToString() });
            _products = result.Products;
            await _dataGrid.GoToPage(0);
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

    private async Task AddProducts() => NavigationManager.NavigateTo("/products/add");

    private async Task EditProduct(Guid id) => NavigationManager.NavigateTo($"/products/edit/{id}");

    private async Task ExportClick() => NavigationManager.NavigateTo("/export/products", true);
}