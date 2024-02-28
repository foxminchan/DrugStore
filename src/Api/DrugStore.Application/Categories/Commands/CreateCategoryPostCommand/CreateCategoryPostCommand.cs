using Ardalis.Result;
using DrugStore.Domain.SharedKernel;
using Microsoft.AspNetCore.Http;

namespace DrugStore.Application.Categories.Commands.CreateCategoryPostCommand;

public sealed record PostCreateRequest(Guid CategoryId, string Title, string? Detail);

public sealed record CreateCategoryPostCommand(PostCreateRequest PostRequest, IFormFile? Image) : ICommand<Result<Guid>>;
