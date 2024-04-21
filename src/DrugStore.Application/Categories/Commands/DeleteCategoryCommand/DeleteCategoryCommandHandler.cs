using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Application.Abstractions.Commands;
using DrugStore.Domain.CategoryAggregate;
using DrugStore.Domain.CategoryAggregate.Specifications;
using DrugStore.Persistence.Repositories;

namespace DrugStore.Application.Categories.Commands.DeleteCategoryCommand;

public sealed class DeleteCategoryCommandHandler(IRepository<Category> repository)
    : ICommandHandler<DeleteCategoryCommand, Result>
{
    public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var spec = new CategoryByIdSpec(request.Id);
        var category = await repository.GetByIdAsync(spec, cancellationToken);
        Guard.Against.NotFound(request.Id, category);
        await repository.DeleteAsync(category, cancellationToken);
        return Result.Success();
    }
}