using DrugStore.BackOffice.Components.Pages.Products.Requests;
using DrugStore.BackOffice.Components.Pages.Products.Response;
using DrugStore.BackOffice.Helpers;
using Refit;

namespace DrugStore.BackOffice.Components.Pages.Products.Services;

public interface IProductsApi
{
    [Get("/products")]
    Task<ListProducts> ListProductsAsync([Query] FilterHelper filter);

    [Get("/products/{id}")]
    Task<Product> GetProductAsync(Guid id);

    [Multipart]
    [Post("/products")]
    Task CreateProductAsync([Body(buffered: true)] CreateProduct product, [Header("X-Idempotency-Key")] Guid key);

    [Delete("/products/{id}")]
    Task DeleteProductAsync(Guid id, [Query] bool? isDeleteImage);
}