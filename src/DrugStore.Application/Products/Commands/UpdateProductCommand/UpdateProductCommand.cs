using Ardalis.Result;
using DrugStore.Application.Products.ViewModels;
using DrugStore.Domain.CategoryAggregate.Primitives;
using DrugStore.Domain.ProductAggregate.Enums;
using DrugStore.Domain.ProductAggregate.Primitives;
using DrugStore.Domain.ProductAggregate.ValueObjects;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Products.Commands.UpdateProductCommand;

public sealed record UpdateProductCommand(
    ProductId Id,
    string Title,
    string? ProductCode,
    string? Detail,
    ProductStatus Status,
    int Quantity,
    CategoryId? CategoryId,
    ProductPrice ProductPrice) : ICommand<Result<ProductVm>>;