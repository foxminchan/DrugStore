using Ardalis.GuardClauses;
using Ardalis.Result;

using DrugStore.Domain.CategoryAggregate;
using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure.Storage.Cloudinary;
using DrugStore.Persistence;

namespace DrugStore.Application.Categories.Commands.CreateCategoryPostCommand;

public sealed class CreateCategoryPostCommandHandler(
    Repository<Category> repository,
    ICloudinaryService cloudinary) : ICommandHandler<CreateCategoryPostCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateCategoryPostCommand request, CancellationToken cancellationToken)
    {
        var category = await repository.GetByIdAsync(request.PostRequest.CategoryId, cancellationToken);
        Guard.Against.NotFound(request.PostRequest.CategoryId, category);

        var post = new Post(
            request.PostRequest.Title,
            request.PostRequest.Detail,
            null,
            request.PostRequest.CategoryId);

        if (request.Image is { })
        {
            var image = await cloudinary.AddPhotoAsync(request.Image, "post");
            post.Image = image.Value.Url;
        }

        category.AddPost(post);

        await repository.UpdateAsync(category, cancellationToken);

        return Result<Guid>.Success(post.Id);
    }
}
