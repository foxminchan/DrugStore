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
    [Inject] private ICategoriesApi CategoriesApi { get; set; } = default!;

    [Inject] private IProductsApi ProductsApi { get; set; } = default!;

    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    [Inject] private NotificationService NotificationService { get; set; } = default!;

    private int _count;

    private bool _busy;

    private bool _error;

    private List<Category> _categories = [];

    private Category _selectedCategory = new();

    private readonly CreateProduct _product = new();

    private async Task OnSubmit(CreateProduct product)
    {
        try
        {
            _busy = true;
            await ProductsApi.CreateProductAsync(product, Guid.NewGuid());
            NotificationService.Notify(new()
            {
                Severity = NotificationSeverity.Success,
                Summary = "Success",
                Detail = "Product created successfully"
            });
            NavigationManager.NavigateTo("/products");
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


    private async Task LoadData()
    {
        try
        {
            _busy = true;
            var result = await CategoriesApi.ListCategoriesAsync();
            _categories = result.Categories;
            _count = _categories.Count;
            _selectedCategory = _categories[0];
            await InvokeAsync(StateHasChanged);
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

    private async Task CancelButtonClick(MouseEventArgs arg) => NavigationManager.NavigateTo("/products");

    private static bool ValidateProductName(string? name)
        => !string.IsNullOrEmpty(name) && name.Length <= DataTypeLength.DefaultLength;

    private static bool ValidateProductCode(string? code)
    {
        if (string.IsNullOrEmpty(code)) return true;
        return code.Length <= DataTypeLength.SmallLength;
    }

    private static bool ValidateProductDetail(string? detail)
    {
        if (string.IsNullOrEmpty(detail)) return true;
        return detail.Length <= DataTypeLength.MaxLength;
    }

    private static bool ValidateProductQuantity(int productQuantity) => productQuantity >= 0;

    private static bool ValidateProductCategory(Guid? categoryId) => categoryId is null;

    private static bool ValidateProductPrice(decimal productPrice) => productPrice >= 0;

    private bool ValidateProductPriceSale(decimal productPriceSale)
        => productPriceSale >= 0 && productPriceSale <= _product.Price;

    private static bool ValidateProductImage(IFormFile? image)
    {
        if (image is null) return true;
        return image.ContentType.Contains("image") && image.Length <= DataTypeLength.MaxFileSize;
    }

    private bool ValidateProductImageAlt(string? alt)
        => _product.File is null || (!string.IsNullOrEmpty(alt) && alt.Length <= DataTypeLength.DefaultLength);
}