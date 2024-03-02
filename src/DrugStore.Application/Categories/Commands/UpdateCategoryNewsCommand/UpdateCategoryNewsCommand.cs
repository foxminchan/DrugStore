using Ardalis.Result;
using DrugStore.Application.Categories.ViewModels;
using DrugStore.Domain.CategoryAggregate.Primitives;
using DrugStore.Domain.SharedKernel;
using Microsoft.AspNetCore.Http;

namespace DrugStore.Application.Categories.Commands.UpdateCategoryNewsCommand;

public sealed record NewsUpdateRequest(CategoryId CategoryId, NewsId NewsId, string Title, string Detail, string? ImageUrl);

public sealed record UpdateCategoryNewsCommand(NewsUpdateRequest NewsRequest, IFormFile? ImageFile)
    : ICommand<Result<NewsVm>>;