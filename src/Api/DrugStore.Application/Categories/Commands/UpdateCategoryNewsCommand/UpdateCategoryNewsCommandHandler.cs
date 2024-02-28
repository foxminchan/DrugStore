using Ardalis.GuardClauses;
using Ardalis.Result;

using DrugStore.Application.Categories.ViewModels;
using DrugStore.Domain.CategoryAggregate;
using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure.Storage.Cloudinary;
using DrugStore.Persistence;

using Mapster;

namespace DrugStore.Application.Categories.Commands.UpdateCategoryNewsCommand;

public class UpdateCategoryNewsCommandHandler(
    Repository<Category> repository,
    ICloudinaryService cloudinary) : ICommandHandler<UpdateCategoryNewsCommand, Result<NewsVm>>
{
    public async Task<Result<NewsVm>> Handle(UpdateCategoryNewsCommand request, CancellationToken cancellationToken)
    {
        var category = await repository.GetByIdAsync(request.CategoryId, cancellationToken);
        Guard.Against.NotFound(request.CategoryId, category);

        var news = category.News?.FirstOrDefault(x => x.Id == request.NewsId);
        Guard.Against.NotFound(request.NewsId, news);

        if (request.ImageUrl is { })
        {
            await DeleteNewsImageAsync(news);
            news.Image = request.ImageUrl;
        }

        if (request.ImageFile is { })
        {
            await DeleteNewsImageAsync(news);
            news.Image = (await cloudinary.AddPhotoAsync(request.ImageFile, "news")).Value.Url;
        }

        news.Update(request.Title, request.Detail, news.Image, request.CategoryId);

        await repository.UpdateAsync(category, cancellationToken);

        return Result<NewsVm>.Success(news.Adapt<NewsVm>());
    }

    private async Task DeleteNewsImageAsync(News news)
    {
        if (news.Image is { })
            await cloudinary.DeletePhotoAsync(news.Image);
    }
}
