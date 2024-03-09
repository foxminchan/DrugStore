using Ardalis.Result;
using Microsoft.AspNetCore.Components;

namespace DrugStore.BackOffice.Components.Pages.Products;

public partial class Index
{
    [Inject] private IProductsApi ProductsApi { get; set; } = default!;

    private readonly List<string> _errorMessages = [];
    private List<ProductResponse> _products = [];
    private bool _loading;

    protected override async Task OnInitializedAsync()
    {
        _loading = true;
        var result = await ProductsApi.GetProductsAsync(new());
        _loading = false;

        if (result.Status == ResultStatus.Ok)
            _products = result.Value;
        else
            _errorMessages.Add("An error occurred while retrieving products. Please try again.");
    }
}