using Ardalis.Result;
using DrugStore.Application.Products.ViewModels;
using DrugStore.Domain.CategoryAggregate.Primitives;
using DrugStore.Domain.ProductAggregate.Enums;
using DrugStore.Domain.ProductAggregate.Primitives;
using DrugStore.Domain.ProductAggregate.ValueObjects;
using DrugStore.Domain.SharedKernel;
using Microsoft.AspNetCore.Http;

namespace DrugStore.Application.Products.Commands.UpdateProductCommand;

public sealed record ProductUpdateRequest(
    ProductId Id,
    string Name,
    string? ProductCode,
    string? Detail,
    ProductStatus Status,
    int Quantity,
    CategoryId? CategoryId,
    ProductPrice ProductPrice,
    List<string>? ImageUrls
);

public sealed record UpdateProductCommand(ProductUpdateRequest ProductRequest, List<IFormFile>? Images)
    : ICommand<Result<ProductVm>>;