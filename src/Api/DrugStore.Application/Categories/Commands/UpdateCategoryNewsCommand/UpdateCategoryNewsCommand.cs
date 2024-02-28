﻿using Ardalis.Result;

using DrugStore.Application.Categories.ViewModels;
using DrugStore.Domain.SharedKernel;

using Microsoft.AspNetCore.Http;

namespace DrugStore.Application.Categories.Commands.UpdateCategoryNewsCommand;

public sealed record NewsUpdateRequest(Guid CategoryId, Guid NewsId, string Title, string Detail, string? ImageUrl);

public record UpdateCategoryNewsCommand(NewsUpdateRequest NewsRequest, IFormFile? ImageFile) 
    : ICommand<Result<NewsVm>>;
