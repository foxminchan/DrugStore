using DrugStore.Domain.CategoryAggregate.Primitives;
using DrugStore.Domain.ProductAggregate.Enums;
using DrugStore.Domain.ProductAggregate.Primitives;
using DrugStore.Domain.ProductAggregate.ValueObjects;

namespace DrugStore.Application.Products.ViewModels;

public sealed record ProductVm(
    ProductId Id,
    string Name,
    string? ProductCode,
    string? Detail,
    ProductStatus Status,
    int Quantity,
    CategoryId? CategoryId,
    ProductPrice Price
);