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
    private bool _busy;

    private int _count;

    private bool _error;

    private List<Category> _categories = [];

    private Category _selectedCategory = new();

    private readonly CreateProduct _product = new();

    [Inject] private ICategoriesApi CategoriesApi { get; set; } = default!;

    [Inject] private IProductsApi ProductsApi { get; set; } = default!;

    [Inject] private DialogService DialogService { get; set; } = default!;

    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    [Inject] private NotificationService NotificationService { get; set; } = default!;

    private async Task OnSubmit(CreateProduct product)
    {
        try
        {
            if (await DialogService.Confirm(MessageContent.SAVE_CHANGES) == true)
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

    private async Task CancelButtonClick(MouseEventArgs arg)
    {
        if (await DialogService.Confirm(MessageContent.DISCARD_CHANGES) == true)
            NavigationManager.NavigateTo("/products");
    }

    private static bool ValidateProductImage(IFormFile? image)
    {
        if (image is null) return true;
        return image.ContentType.Contains("image") && image.Length <= DataTypeLength.MAX_FILE_SIZE;
    }
}