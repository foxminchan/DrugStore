using Ardalis.Result;

using DrugStore.Application.Categories.ViewModels;
using DrugStore.Domain.SharedKernel;

using Microsoft.AspNetCore.Http;

namespace DrugStore.Application.Categories.Commands.UpdateCategoryNewsCommand;

public record UpdateCategoryNewsCommand(
    Guid CategoryId,
    Guid NewsId,
    string Title,
    string Detail,
    IFormFile? ImageFile,
    string? ImageUrl) : ICommand<Result<NewsVm>>;
