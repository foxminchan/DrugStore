using Ardalis.Result;
using DrugStore.Domain.CategoryAggregate.Primitives;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Categories.Commands.CreateCategoryCommand;

public sealed record CategoryCreateRequest(string Title, string? Link);

public sealed record CreateCategoryCommand(Guid RequestId, CategoryCreateRequest CategoryRequest)
    : IdempotencyCommand<Result<CategoryId>>(RequestId);