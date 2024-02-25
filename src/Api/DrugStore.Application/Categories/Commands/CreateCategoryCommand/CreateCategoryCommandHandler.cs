using Ardalis.Result;
using DrugStore.Domain.Category;
using DrugStore.Domain.SharedKernel;
using DrugStore.Persistence;

namespace DrugStore.Application.Categories.Commands.CreateCategoryCommand;

public class CreateCategoryCommandHandler(Repository<Category> repository) 
    : ICommandHandler<CreateCategoryCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category(request.Title, request.Link);
        await repository.AddAsync(category, cancellationToken);
        return Result<Guid>.Success(category.Id);
    }
}
