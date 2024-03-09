using Ardalis.Result;
using DrugStore.BackOffice.Helpers;
using Refit;

namespace DrugStore.BackOffice.Components.Pages.Products;

public interface IProductsApi
{
    [Get("/products")]
    Task<PagedResult<List<ProductResponse>>> GetProductsAsync([Query] FilterHelper filter);

    [Get("/products/{id}")]
    Task<Result<ProductResponse>> GetProductAsync(Guid id);

    [Delete("/products/{id}")]
    Task<Result> DeleteProductAsync(Guid id);
}