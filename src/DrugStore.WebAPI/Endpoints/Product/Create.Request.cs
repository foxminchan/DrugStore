using DrugStore.Domain.CategoryAggregate.Primitives;

namespace DrugStore.WebAPI.Endpoints.Product;

public sealed record CreateProductRequest(
    string? Idempotency,
    string ProductName,
    string? ProductCode,
    string? Detail,
    int Quantity,
    CategoryId? CategoryId,
    decimal Price,
    decimal PriceSale,
    IFormFile? Image,
    string? Alt
);