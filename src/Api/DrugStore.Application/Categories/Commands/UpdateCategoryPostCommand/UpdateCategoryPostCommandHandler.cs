using Ardalis.GuardClauses;
using Ardalis.Result;

using DrugStore.Application.Categories.ViewModels;
using DrugStore.Domain.CategoryAggregate;
using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure.Storage.Cloudinary;
using DrugStore.Persistence;

using Mapster;

namespace DrugStore.Application.Categories.Commands.UpdateCategoryPostCommand;

public sealed class UpdateCategoryPostCommandHandler(
    Repository<Category> repository,
    ICloudinaryService cloudinary) : ICommandHandler<UpdateCategoryPostCommand, Result<PostVm>>
{
    public async Task<Result<PostVm>> Handle(UpdateCategoryPostCommand request, CancellationToken cancellationToken)
    {
        var category = await repository.GetByIdAsync(request.PostRequest.CategoryId, cancellationToken);
        Guard.Against.NotFound(request.PostRequest.CategoryId, category);

        var post = category.Posts?.FirstOrDefault(x => x.Id == request.PostRequest.PostId);
        Guard.Against.NotFound(request.PostRequest.PostId, post);

        if (request.PostRequest.ImageUrl is { })
        {
            await DeletePostImageAsync(post);
            post.Image = request.PostRequest.ImageUrl;
        }

        if (request.ImageFile is { })
        {
            await DeletePostImageAsync(post);
            post.Image = (await cloudinary.AddPhotoAsync(request.ImageFile, "post")).Value.Url;
        }

        post.Update(request.PostRequest.Title, request.PostRequest.Detail, post.Image, request.PostRequest.CategoryId);

        await repository.UpdateAsync(category, cancellationToken);

        return Result<PostVm>.Success(post.Adapt<PostVm>());
    }

    private async Task DeletePostImageAsync(Post post)
    {
        if (post.Image is { })
            await cloudinary.DeletePhotoAsync(post.Image);
    }
}
