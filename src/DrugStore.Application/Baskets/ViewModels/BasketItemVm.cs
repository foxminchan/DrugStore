using DrugStore.Domain.ProductAggregate.Primitives;

namespace DrugStore.Application.Baskets.ViewModels;

public sealed record BasketItemVm(
    ProductId ProductId,
    string? ProductName,
    int Quantity,
    decimal Price,
    decimal Total
);