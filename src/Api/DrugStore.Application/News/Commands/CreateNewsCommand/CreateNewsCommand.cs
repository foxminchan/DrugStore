using Ardalis.Result;
using DrugStore.Domain.SharedKernel;
using Microsoft.AspNetCore.Http;

namespace DrugStore.Application.News.Commands.CreateNewsCommand;

public sealed record CreateNewsCommand(string Title, string Detail, IFormFile? ImageFile, Guid? CategoryId)
    : ICommand<Result<Guid>>;
