using System.Text.Json;
using DrugStore.BackOffice.Components.Pages.Categories.Responses;
using DrugStore.BackOffice.Components.Pages.Categories.Services;
using DrugStore.BackOffice.Components.Pages.Products.Requests;
using DrugStore.BackOffice.Components.Pages.Products.Services;
using DrugStore.BackOffice.Constants;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;

namespace DrugStore.BackOffice.Components.Pages.Products;

public sealed partial class Add
{
    private readonly CreateProduct _product = new();
    private bool _busy;

    private List<Category> _categories = [];

    private int _categoriesCount;

    private bool _error;

    [Inject] private ICategoriesApi CategoriesApi { get; set; } = default!;

    [Inject] private IProductsApi ProductsApi { get; set; } = default!;

    [Inject] private DialogService DialogService { get; set; } = default!;

    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    [Inject] private NotificationService NotificationService { get; set; } = default!;

    [Inject] private ILogger<Add> Logger { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            _busy = true;
            _error = false;

            var categories = await CategoriesApi.ListCategoriesAsync();
            _categories = categories.Categories;
            _categoriesCount = _categories.Count;
        }
        catch (Exception)
        {
            NotificationService.Notify(new()
                { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Unable to load Categories" });
            NavigationManager.NavigateTo("/products");
        }
        finally
        {
            _busy = false;
        }
    }

    private async Task OnSubmit(CreateProduct product)
    {
        try
        {
            if (await DialogService.Confirm(MessageContent.SAVE_CHANGES) == true)
            {
                _busy = true;

                Logger.LogInformation("[{Page}] Product information: {Product}", nameof(Add),
                    JsonSerializer.Serialize(product));

                await ProductsApi.CreateProductAsync(
                    product.Name,
                    product.ProductCode,
                    product.Detail,
                    product.Quantity,
                    product.CategoryId!.Value,
                    product.Price,
                    product.PriceSale,
                    product.File,
                    product.Alt,
                    Guid.NewGuid()
                );

                NotificationService.Notify(new()
                {
                    Severity = NotificationSeverity.Success,
                    Summary = "Success",
                    Detail = "Product created successfully"
                });
                NavigationManager.NavigateTo("/products");
            }
        }
        catch (Exception)
        {
            NotificationService.Notify(new()
                { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Unable to create Product" });
            _error = true;
        }
        finally
        {
            _busy = false;
        }
    }

    private async Task CancelButtonClick(MouseEventArgs arg)
    {
        if (await DialogService.Confirm(MessageContent.DISCARD_CHANGES) == true)
            NavigationManager.NavigateTo("/products");
    }
}