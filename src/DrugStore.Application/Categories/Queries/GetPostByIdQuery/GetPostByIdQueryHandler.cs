using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Application.Categories.ViewModels;
using DrugStore.Domain.CategoryAggregate;
using DrugStore.Domain.SharedKernel;
using DrugStore.Persistence;
using Mapster;

namespace DrugStore.Application.Categories.Queries.GetPostByIdQuery;

public sealed class GetPostByIdQueryHandler(Repository<Category> repository)
    : ICommandHandler<GetPostByIdQuery, Result<PostVm>>
{
    public async Task<Result<PostVm>> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await repository.GetByIdAsync(request.CategoryId, cancellationToken);
        Guard.Against.NotFound(request.CategoryId, category);

        var post = category.Posts?.FirstOrDefault(p => p.Id == request.PostId);
        Guard.Against.NotFound(request.PostId, post);

        return Result<PostVm>.Success(post.Adapt<PostVm>());
    }
}