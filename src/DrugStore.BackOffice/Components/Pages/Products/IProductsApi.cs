using Ardalis.Result;
using Refit;

namespace DrugStore.BackOffice.Components.Pages.Products;

public interface IProductsApi
{
    [Get("/products")]
    Task<Result<List<ProductResponse>>> GetProducts();
}