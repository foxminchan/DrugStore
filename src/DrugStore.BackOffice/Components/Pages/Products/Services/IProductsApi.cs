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
    Task CreateProductAsync(
        [AliasAs("name")] string? name,
        [AliasAs("productCode")] string? productCode,
        [AliasAs("detail")] string? detail,
        [AliasAs("quantity")] int quantity,
        [AliasAs("categoryId")] Guid? categoryId,
        [AliasAs("price")] decimal price,
        [AliasAs("priceSale")] decimal priceSale,
        [AliasAs("image")] IFormFile? file,
        [AliasAs("alt")] string? alt,
        [Header("X-Idempotency-Key")] Guid key
    );

    [Multipart]
    [Put("/products")]
    Task UpdateProductAsync(
        [AliasAs("id")] Guid id,
        [AliasAs("name")] string? name,
        [AliasAs("productCode")] string? productCode,
        [AliasAs("detail")] string? detail,
        [AliasAs("quantity")] int quantity,
        [AliasAs("categoryId")] Guid? categoryId,
        [AliasAs("price")] decimal price,
        [AliasAs("priceSale")] decimal priceSale,
        [AliasAs("imageUrl")] string? imageUrl,
        [AliasAs("image")] IFormFile? file,
        [AliasAs("alt")] string? alt
    );

    [Delete("/products/{id}")]
    Task DeleteProductAsync(Guid id, [Query] bool? isDeleteImage);
}