using DrugStore.BackOffice.Components.Pages.Products.Requests;
using DrugStore.BackOffice.Components.Pages.Products.Response;
using DrugStore.BackOffice.Helpers;
using Refit;

namespace DrugStore.BackOffice.Components.Pages.Products.Services;

public interface IProductsApi
{
    [Get("/products")]
    Task<ListProducts> GetProductsAsync([Query] FilterHelper filter);

    [Get("/products/{id}")]
    Task<Product> GetProductAsync(Guid id);

    [Post("/products")]
    Task<Guid> CreateProductAsync(ProductPayload productInfoRequest, [Header("X-Idempotency-Key")] string key);

    [Multipart]
    [Put("/products/images/{id}")]
    Task<Guid> UpdateProductImageAsync(Guid id, ImagePayload productImageRequest);

    [Delete("/products/{id}")]
    Task DeleteProductAsync(Guid id, [Query] bool? isDeleteImage);
}