using DrugStore.Domain.Product;

namespace DrugStore.Application.Products.ViewModel;

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
