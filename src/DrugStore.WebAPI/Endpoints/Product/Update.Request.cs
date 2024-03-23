using DrugStore.Domain.CategoryAggregate.Primitives;
using DrugStore.Domain.ProductAggregate.Primitives;

namespace DrugStore.WebAPI.Endpoints.Product;

public sealed record UpdateProductRequest(
    ProductId Id,
    string Name,
    string? ProductCode,
    string? Detail,
    int Quantity,
    CategoryId? CategoryId,
    decimal Price,
    decimal PriceSale,
    bool IsDeleteImage,
    IFormFile? Image,
    string? Alt
);