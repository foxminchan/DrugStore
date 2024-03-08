using Ardalis.Result;
using DrugStore.Application.Products.ViewModels;
using DrugStore.Domain.CategoryAggregate.Primitives;
using DrugStore.Domain.ProductAggregate.Primitives;
using DrugStore.Domain.ProductAggregate.ValueObjects;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Products.Commands.UpdateProductCommand;

public sealed record UpdateProductCommand(
    ProductId Id,
    string Name,
    string? ProductCode,
    string? Detail,
    int Quantity,
    CategoryId? CategoryId,
    ProductPrice ProductPrice,
    string? ImageUrl
) : ICommand<Result<ProductVm>>;