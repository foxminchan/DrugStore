using Ardalis.GuardClauses;
using Ardalis.Result;

using DrugStore.Domain.CategoryAggregate;
using DrugStore.Domain.SharedKernel;
using DrugStore.Persistence;

namespace DrugStore.Application.Categories.Commands.DeleteCategoryCommand;

public sealed class DeleteCategoryCommandHandler(Repository<Category> repository)
    : ICommandHandler<DeleteCategoryCommand, Result>
{
    public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await repository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, category);
        await repository.DeleteAsync(category, cancellationToken);
        return Result.Success();
    }
}
