using Ardalis.Result;
using DrugStore.Domain.CategoryAggregate;
using DrugStore.Domain.SharedKernel;
using DrugStore.Persistence;

namespace DrugStore.Application.Categories.Commands.CreateCategoryCommand;

public sealed class CreateCategoryCommandHandler(Repository<Category> repository)
    : IIdempotencyCommandHandler<CreateCategoryCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        Category category = new(request.CategoryRequest.Title, request.CategoryRequest.Link);
        await repository.AddAsync(category, cancellationToken);
        return Result<Guid>.Success(category.Id);
    }
}