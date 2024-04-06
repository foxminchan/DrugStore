using Ardalis.Result;
using DrugStore.Application.Abstractions.Commands;
using DrugStore.Domain.CategoryAggregate.Primitives;

namespace DrugStore.Application.Categories.Commands.CreateCategoryCommand;

public sealed record CreateCategoryCommand(Guid RequestId, string Name, string? Description)
    : IdempotencyCommand<Result<CategoryId>>(RequestId);