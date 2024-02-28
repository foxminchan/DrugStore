using DrugStore.Domain.ProductAggregate;

namespace DrugStore.Application.Products.ViewModels;

public sealed record ProductVm(
    Guid Id,
    string Title,
    string? ProductCode,
    string? Detail,
    ProductStatus Status,
    int Quantity,
    Guid? CategoryId,
    ProductPrice Price,
    DateTime CreatedDate,
    DateTime? UpdateDate,
    Guid Version);
