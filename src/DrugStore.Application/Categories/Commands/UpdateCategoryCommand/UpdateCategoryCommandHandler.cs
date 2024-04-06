using System.Text.Json;
using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Application.Abstractions.Commands;
using DrugStore.Application.Categories.ViewModels;
using DrugStore.Domain.CategoryAggregate;
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
        var category = await repository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, category);
        category.Update(request.Name, request.Description);
        logger.LogInformation("[{Command}] Category information: {Category}",
            nameof(UpdateCategoryCommand), JsonSerializer.Serialize(category));
        await repository.UpdateAsync(category, cancellationToken);
        return Result<CategoryVm>.Success(mapper.Map<CategoryVm>(category));
    }
}