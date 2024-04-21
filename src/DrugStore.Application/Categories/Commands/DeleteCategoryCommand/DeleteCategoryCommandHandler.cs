using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Domain.CategoryAggregate;
using DrugStore.Domain.CategoryAggregate.Specifications;
using DrugStore.Domain.SharedKernel;
using DrugStore.Persistence.Repositories;

namespace DrugStore.Application.Categories.Commands.DeleteCategoryCommand;

public sealed class DeleteCategoryCommandHandler(IRepository<Category> repository)
    : ICommandHandler<DeleteCategoryCommand, Result>
{
    public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        CategoryByIdSpec spec = new(request.Id);
        var category = await repository.GetByIdAsync(spec, cancellationToken);
        Guard.Against.NotFound(request.Id, category);
        await repository.DeleteAsync(category, cancellationToken);
        return Result.Success();
    }
}