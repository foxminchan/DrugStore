namespace DrugStore.Application.Products.ViewModel;

public sealed record ProductVm(
    Guid Id,
    string Title,
    string? ProductCode,
    string? Detail,
    bool Status,
    int Quantity,
    Guid? CategoryId,
    decimal OriginalPrice,
    decimal Price,
    decimal? PriceSale);
