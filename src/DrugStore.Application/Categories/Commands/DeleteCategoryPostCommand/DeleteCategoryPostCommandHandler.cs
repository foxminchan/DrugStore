using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Domain.CategoryAggregate;
using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure.Storage.Cloudinary;
using DrugStore.Persistence;

namespace DrugStore.Application.Categories.Commands.DeleteCategoryPostCommand;

public sealed class DeleteCategoryPostCommandHandler(
    Repository<Category> repository,
    ICloudinaryService cloudinary) : ICommandHandler<DeleteCategoryPostCommand, Result>
{
    public async Task<Result> Handle(DeleteCategoryPostCommand request, CancellationToken cancellationToken)
    {
        var category = await repository.GetByIdAsync(request.CategoryId, cancellationToken);
        Guard.Against.NotFound(request.CategoryId, category);

        var post = category.Posts?.FirstOrDefault(x => x.Id == request.PostId);
        Guard.Against.NotFound(request.PostId, post);

        if (post.Image is { }) await cloudinary.DeletePhotoAsync(post.Image);

        category.Posts?.Remove(post);
        await repository.UpdateAsync(category, cancellationToken);

        return Result.Success();
    }
}