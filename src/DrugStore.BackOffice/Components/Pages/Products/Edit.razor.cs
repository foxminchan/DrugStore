using System.Text.Json;
using DrugStore.BackOffice.Components.Pages.Categories.Responses;
using DrugStore.BackOffice.Components.Pages.Categories.Services;
using DrugStore.BackOffice.Components.Pages.Products.Requests;
using DrugStore.BackOffice.Components.Pages.Products.Services;
using DrugStore.BackOffice.Constants;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace DrugStore.BackOffice.Components.Pages.Products;

public partial class Edit
{
    private bool _busy;

    private bool _error;

    private int _categoriesCount;

    private List<Category> _categories = [];

    private readonly UpdateProduct _product = new();

    [Inject] private ILogger<Edit> Logger { get; set; } = default!;

    [Inject] private IProductsApi ProductsApi { get; set; } = default!;

    [Inject] private DialogService DialogService { get; set; } = default!;

    [Inject] private ICategoriesApi CategoriesApi { get; set; } = default!;

    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    [Inject] private NotificationService NotificationService { get; set; } = default!;

    [Parameter] public required string Id { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            _busy = true;
            _error = false;

            var categories = await CategoriesApi.ListCategoriesAsync();
            _categories = categories.Categories;
            _categoriesCount = _categories.Count;

            var product = await ProductsApi.GetProductAsync(Guid.Parse(Id));
            _product.Id = product.Id.ToString();
            _product.Name = product.Name;
            _product.ProductCode = product.ProductCode;
            _product.Detail = product.Detail;
            _product.Quantity = product.Quantity;
            _product.CategoryId = Guid.TryParse(product.Category, out var categoryId) ? categoryId : null;
            _product.Price = product.Price.Price;
            _product.PriceSale = product.Price.PriceSale;
            _product.ImageUrl = product.Image?.ImageUrl;
            _product.Alt = product.Image?.Alt;
        }
        catch (Exception)
        {
            NotificationService.Notify(new()
                { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Unable to load Product" });
            NavigationManager.NavigateTo("/products");
        }
        finally
        {
            _busy = false;
        }
    }

    private async Task OnSubmit(UpdateProduct product)
    {
        try
        {
            if (await DialogService.Confirm(MessageContent.SAVE_CHANGES) == true)
            {
                _busy = true;

                Logger.LogInformation("[{Page}] Product information: {Product}", nameof(Edit),
                    JsonSerializer.Serialize(product));

                await ProductsApi.UpdateProductAsync(
                    Guid.Parse(product.Id), 
                    product.Name,
                    product.ProductCode,
                    product.Detail,
                    product.Quantity,
                    product.CategoryId!.Value,
                    product.Price,
                    product.PriceSale,
                    product.ImageUrl,
                    product.File,
                    product.Alt
                );

                NotificationService.Notify(new()
                {
                    Severity = NotificationSeverity.Success,
                    Summary = "Success",
                    Detail = "Product updated successfully"
                });
                NavigationManager.NavigateTo("/products");
            }
        }
        catch (Exception)
        {
            NotificationService.Notify(new()
                { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Unable to update Product" });
        }
        finally
        {
            _busy = false;
        }
    }

    private async Task DeleteImage()
    {
        if (await DialogService.Confirm(MessageContent.DELETE_IMAGE) == true)
        {
            _product.ImageUrl = null;
            _product.Alt = null;
        }
    }

    private async Task CancelButtonClick()
    {
        if (await DialogService.Confirm(MessageContent.DISCARD_CHANGES) == true)
            NavigationManager.NavigateTo("/products");
    }
}