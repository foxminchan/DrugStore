using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Application.Categories.ViewModels;
using DrugStore.Domain.CategoryAggregate;
using DrugStore.Domain.SharedKernel;
using DrugStore.Persistence;
using Mapster;

namespace DrugStore.Application.Categories.Commands.UpdateCategoryCommand;

public sealed class UpdateCategoryCommandHandler(Repository<Category> repository)
    : ICommandHandler<UpdateCategoryCommand, Result<CategoryVm>>
{
    public async Task<Result<CategoryVm>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await repository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, category);
        category.Update(request.Title, request.Link);
        await repository.UpdateAsync(category, cancellationToken);
        return Result<CategoryVm>.Success(category.Adapt<CategoryVm>());
    }
}