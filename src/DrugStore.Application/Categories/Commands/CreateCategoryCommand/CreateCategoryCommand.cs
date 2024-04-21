using Ardalis.Result;
using DrugStore.Domain.CategoryAggregate.Primitives;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Categories.Commands.CreateCategoryCommand;

public sealed record CreateCategoryCommand(Guid RequestId, string Name, string? Description)
    : IdempotencyCommand<Result<CategoryId>>(RequestId);