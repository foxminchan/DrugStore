using DrugStore.Domain.ProductAggregate.Primitives;

namespace DrugStore.Application.Report.ViewModels;

public sealed record TopProductByMonthVm(
    ProductId Id,
    string Name,
    int TotalQuantity
);