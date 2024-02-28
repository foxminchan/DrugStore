using Ardalis.Result;

using DrugStore.Domain.SharedKernel;

using Microsoft.AspNetCore.Http;

namespace DrugStore.Application.Categories.Commands.CreateCategoryNewsCommand;

public sealed record NewsCreateRequest(Guid CategoryId, string Title, string Detail);

public sealed record CreateCategoryNewsCommand(NewsCreateRequest NewsRequest, IFormFile? ImageFile) : ICommand<Result<Guid>>;
