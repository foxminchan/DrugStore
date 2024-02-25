using Ardalis.Result;
using DrugStore.Application.Products.ViewModel;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Products.Commands.UpdateProductCommand;

public sealed record UpdateProductCommand(
    Guid Id,
    string Title,
    string? ProductCode,
    string? Detail,
    bool Status,
    int Quantity,
    Guid? CategoryId,
    decimal OriginalPrice,
    decimal Price,
    decimal? PriceSale) : ICommand<Result<ProductVm>>;
