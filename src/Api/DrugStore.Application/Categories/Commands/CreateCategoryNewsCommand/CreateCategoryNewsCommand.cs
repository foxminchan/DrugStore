using Ardalis.Result;

using DrugStore.Domain.SharedKernel;

using Microsoft.AspNetCore.Http;

namespace DrugStore.Application.Categories.Commands.CreateCategoryNewsCommand;

public sealed record CreateCategoryNewsCommand(
    Guid CategoryId,
    string Title,
    string Detail,
    IFormFile? ImageFile
) : ICommand<Result<Guid>>;
