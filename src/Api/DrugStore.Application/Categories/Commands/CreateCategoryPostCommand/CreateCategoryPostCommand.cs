using Ardalis.Result;
using DrugStore.Domain.SharedKernel;
using Microsoft.AspNetCore.Http;

namespace DrugStore.Application.Categories.Commands.CreateCategoryPostCommand;

public sealed record PostCreateRequest(Guid CategoryId, string Title, string? Detail);

public sealed record CreateCategoryPostCommand(Guid RequestId, PostCreateRequest PostRequest, IFormFile? Image)
    : IdempotencyCommand<Result<Guid>>(RequestId);
