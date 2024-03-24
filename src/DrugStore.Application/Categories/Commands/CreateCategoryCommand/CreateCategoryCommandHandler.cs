using System.Text.Json;
using Ardalis.Result;
using DrugStore.Domain.CategoryAggregate;
using DrugStore.Domain.CategoryAggregate.Primitives;
using DrugStore.Domain.SharedKernel;
using Microsoft.Extensions.Logging;

namespace DrugStore.Application.Categories.Commands.CreateCategoryCommand;

public sealed class CreateCategoryCommandHandler(
    IRepository<Category> repository,
    ILogger<CreateCategoryCommandHandler> logger)
    : IIdempotencyCommandHandler<CreateCategoryCommand, Result<CategoryId>>
{
    public async Task<Result<CategoryId>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        Category category = new(request.Name, request.Description);
        logger.LogInformation("[{Command}] Category information: {Category}",
            nameof(CreateCategoryCommand), JsonSerializer.Serialize(category));
        await repository.AddAsync(category, cancellationToken);
        return Result<CategoryId>.Success(category.Id);
    }
}