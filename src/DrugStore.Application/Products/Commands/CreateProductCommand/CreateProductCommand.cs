using Ardalis.Result;
using DrugStore.Domain.CategoryAggregate.Primitives;
using DrugStore.Domain.ProductAggregate.Primitives;
using DrugStore.Domain.ProductAggregate.ValueObjects;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Products.Commands.CreateProductCommand;

public sealed record ProductCreateRequest(
    string Name,
    string? ProductCode,
    string? Detail,
    int Quantity,
    CategoryId? CategoryId,
    ProductPrice ProductPrice
);

public sealed record CreateProductCommand(
    Guid RequestId,
    ProductCreateRequest ProductRequest) : IdempotencyCommand<Result<ProductId>>(RequestId);