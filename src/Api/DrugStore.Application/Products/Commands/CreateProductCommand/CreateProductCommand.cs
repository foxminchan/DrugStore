using Ardalis.Result;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Products.Commands.CreateProductCommand;

public sealed record CreateProductCommand(
    string Title,
    string? ProductCode,
    string? Detail,
    bool Status,
    int Quantity,
    Guid? CategoryId,
    decimal OriginalPrice,
    decimal Price,
    decimal? PriceSale) : ICommand<Result<Guid>>;
