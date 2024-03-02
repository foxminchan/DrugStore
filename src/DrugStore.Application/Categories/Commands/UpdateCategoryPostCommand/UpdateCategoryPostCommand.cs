using Ardalis.Result;
using DrugStore.Application.Categories.ViewModels;
using DrugStore.Domain.CategoryAggregate.Primitives;
using DrugStore.Domain.SharedKernel;
using Microsoft.AspNetCore.Http;

namespace DrugStore.Application.Categories.Commands.UpdateCategoryPostCommand;

public sealed record PostUpdateRequest(CategoryId CategoryId, PostId PostId, string Title, string? Detail, string? ImageUrl);

public sealed record UpdateCategoryPostCommand(PostUpdateRequest PostRequest, IFormFile? ImageFile)
    : ICommand<Result<PostVm>>;