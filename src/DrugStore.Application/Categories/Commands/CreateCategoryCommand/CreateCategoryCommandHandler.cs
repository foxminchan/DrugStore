using Ardalis.Result;
using DrugStore.Domain.CategoryAggregate;
using DrugStore.Domain.CategoryAggregate.Primitives;
using DrugStore.Domain.SharedKernel;
using DrugStore.Persistence;

namespace DrugStore.Application.Categories.Commands.CreateCategoryCommand;

public sealed class CreateCategoryCommandHandler(Repository<Category> repository)
    : IIdempotencyCommandHandler<CreateCategoryCommand, Result<CategoryId>>
{
    public async Task<Result<CategoryId>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        Category category = new(request.CategoryRequest.Title, request.CategoryRequest.Link);
        await repository.AddAsync(category, cancellationToken);
        return Result<CategoryId>.Success(category.Id);
    }
}