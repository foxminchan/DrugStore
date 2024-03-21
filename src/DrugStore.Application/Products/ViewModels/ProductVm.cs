using DrugStore.Domain.ProductAggregate.Primitives;
using DrugStore.Domain.ProductAggregate.ValueObjects;

namespace DrugStore.Application.Products.ViewModels;

public sealed record ProductVm(
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