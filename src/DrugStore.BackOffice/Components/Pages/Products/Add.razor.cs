using Ardalis.Result;
using DrugStore.BackOffice.Components.Pages.Categories;
using DrugStore.BackOffice.Helpers;
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

    private readonly List<string> _errors = [];
    private List<CategoryResponse> _categories = [];
    private readonly ProductCreateRequest _product = new();
    private bool _busy;

    private async Task OnSubmit(ProductCreateRequest product)
    {
        try
        {
            _busy = true;

            var newProductResult = await ProductsApi.CreateProductAsync(product.Product, Guid.NewGuid().ToString());

            if (newProductResult.Status == ResultStatus.Ok)
                await HandleProductCreationSuccess(newProductResult.Value, product.Image);
            else
                HandleProductCreationFailure(
                    [.. newProductResult.ValidationErrors],
                    "An error occurred while adding the product. Please try again."
                );
        }
        catch (Exception ex)
        {
            _errors.Add(ex.Message);
        }
        finally
        {
            _busy = false;
        }
    }

    private async Task HandleProductCreationSuccess(Guid id, ProductImageRequest image)
    {
        if (image.File is null)
        {
            HandleSuccess("Product added successfully.");
            NavigationManager.NavigateTo("/products");
        }
        else
        {
            var imageResult = await ProductsApi.UpdateProductImageAsync(id, image);

            if (imageResult.Status == ResultStatus.Ok)
            {
                HandleSuccess("Product added successfully.");
                NavigationManager.NavigateTo("/products");
            }
            else
            {
                HandleProductCreationFailure(
                    [.. imageResult.ValidationErrors],
                    "An error occurred while adding the product image. Please try again."
                );
            }
        }
    }

    private void HandleProductCreationFailure(List<ValidationError> validationErrors, string defaultMessage)
    {
        foreach (var error in validationErrors) _errors.Add(error.ErrorMessage);

        if (validationErrors.Count != 0) return;

        _errors.Add(defaultMessage);
    }

    private void HandleSuccess(string message) =>
        NotificationService.Notify(new()
        {
            Severity = NotificationSeverity.Success,
            Summary = "Success",
            Detail = message
        });


    private async Task LoadData()
    {
        _busy = true;
        var result = await CategoriesApi.GetCategoriesAsync();
        _busy = false;
        if (result.Status == ResultStatus.Ok)
        {
            _categories = result.Value;
        }
        else
        {
            NotificationService.Notify(new()
            {
                Severity = NotificationSeverity.Error,
                Summary = "Error",
                Detail = "An error occurred while loading categories. Please try again."
            });
            NavigationManager.NavigateTo("/products");
        }

        await InvokeAsync(StateHasChanged);
    }

    private async Task CancelButtonClick(MouseEventArgs arg)
    {
        NavigationManager.NavigateTo("/products");
        await Task.CompletedTask;
    }

    private static bool ValidateProductName(string? name)
        => !string.IsNullOrEmpty(name) && name.Length <= DataLengthHelper.DefaultLength;

    private static bool ValidateProductCode(string? code)
    {
        if (string.IsNullOrEmpty(code)) return true;

        return code.Length <= DataLengthHelper.SmallLength;
    }

    private static bool ValidateProductDetail(string? detail)
    {
        if (string.IsNullOrEmpty(detail)) return true;
        return detail.Length <= DataLengthHelper.MaxLength;
    }

    private static bool ValidateProductQuantity(int productQuantity) => productQuantity >= 0;

    private static bool ValidateProductCategory(Guid? categoryId) => categoryId is null;

    private static bool ValidateProductPrice(decimal productPrice) => productPrice >= 0;

    private bool ValidateProductPriceSale(decimal productPriceSale)
        => productPriceSale >= 0 && productPriceSale <= _product.Product.ProductPrice.Price;

    private static bool ValidateProductImage(IFormFile? image)
    {
        if (image is null) return true;
        return image.ContentType.Contains("image") && image.Length <= DataLengthHelper.MaxFileSize;
    }

    private bool ValidateProductImageAlt(string? alt)
        => _product.Image.File is null || (!string.IsNullOrEmpty(alt) && alt.Length <= DataLengthHelper.DefaultLength);
}