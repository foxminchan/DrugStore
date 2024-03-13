using DrugStore.Domain.ProductAggregate.Primitives;
using DrugStore.Domain.ProductAggregate.ValueObjects;

namespace DrugStore.WebAPI.Endpoints.Product;

public sealed record ProductDto(
    ProductId Id,
    string? Name,
    string? ProductCode,
    string? Detail,
    string? Status,
    int Quantity,
    string? Category,
    ProductPrice? Price,
    ProductImage? Image
);