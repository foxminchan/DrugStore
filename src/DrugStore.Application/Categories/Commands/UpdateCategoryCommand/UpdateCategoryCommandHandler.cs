﻿using System.Text.Json;
using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Application.Categories.ViewModels;
using DrugStore.Domain.CategoryAggregate;
using DrugStore.Domain.CategoryAggregate.Specifications;
using DrugStore.Domain.SharedKernel;
using DrugStore.Persistence.Repositories;
using MapsterMapper;
using Microsoft.Extensions.Logging;

namespace DrugStore.Application.Categories.Commands.UpdateCategoryCommand;

public sealed class UpdateCategoryCommandHandler(
    IMapper mapper,
    IRepository<Category> repository,
    ILogger<UpdateCategoryCommandHandler> logger) : ICommandHandler<UpdateCategoryCommand, Result<CategoryVm>>
{
    public async Task<Result<CategoryVm>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        CategoryByIdSpec spec = new(request.Id);
        var category = await repository.GetByIdAsync(spec, cancellationToken);
        Guard.Against.NotFound(request.Id, category);
        category.Update(request.Name, request.Description);
        logger.LogInformation("[{Command}] Category information: {Category}",
            nameof(UpdateCategoryCommand), JsonSerializer.Serialize(category));
        await repository.UpdateAsync(category, cancellationToken);
        return Result<CategoryVm>.Success(mapper.Map<CategoryVm>(category));
    }
}