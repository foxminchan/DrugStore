using Ardalis.Result;
using DrugStore.Application.News.ViewModel;
using DrugStore.Domain.SharedKernel;
using Microsoft.AspNetCore.Http;

namespace DrugStore.Application.News.Commands.UpdateNewsCommand;

public sealed record UpdateNewsCommand(
    Guid Id, 
    string Title, 
    string Detail, 
    IFormFile? ImageFile, 
    string? ImageUrl,
    Guid? CategoryId) : ICommand<Result<NewsVm>>;
