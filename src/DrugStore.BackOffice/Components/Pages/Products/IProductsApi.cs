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

    [Post("/products")]
    Task<Result<Guid>> CreateProductAsync(ProductInfoRequest productInfoRequest, [Header("X-Idempotency-Key")] string key);

    [Multipart]
    [Put("/products/images/{id}")]
    Task<Result<Guid>> UpdateProductImageAsync(Guid id, ProductImageRequest productImageRequest);

    [Delete("/products/{id}")]
    Task<Result> DeleteProductAsync(Guid id);
}