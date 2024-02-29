using Ardalis.Result;

using DrugStore.Application.Products.ViewModels;
using DrugStore.Domain.ProductAggregate;
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
    ProductPrice ProductPrice) : ICommand<Result<ProductVm>>;
