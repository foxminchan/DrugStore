using DrugStore.Domain.ProductAggregate.Primitives;

namespace DrugStore.WebAPI.Endpoints.Basket;

public sealed record BasketItemDto(
    ProductId ProductId,
    string? ProductName,
    int Quantity,
    decimal Price,
    decimal Total
);