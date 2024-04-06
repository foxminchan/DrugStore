using Ardalis.Result;
using DrugStore.Application.Abstractions.Commands;
using DrugStore.Domain.CategoryAggregate.Primitives;
using DrugStore.Domain.ProductAggregate.Primitives;
using DrugStore.Domain.ProductAggregate.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace DrugStore.Application.Products.Commands.CreateProductCommand;

public sealed record CreateProductCommand(
    Guid RequestId,
    string Name,
    string? ProductCode,
    string? Detail,
    int Quantity,
    CategoryId? CategoryId,
    ProductPrice ProductPrice,
    IFormFile? Image,
    string? Alt) : IdempotencyCommand<Result<ProductId>>(RequestId);