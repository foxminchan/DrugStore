using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Application.Categories.ViewModels;
using DrugStore.Domain.CategoryAggregate;
using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure.Storage.Cloudinary;
using DrugStore.Persistence;
using Mapster;

namespace DrugStore.Application.Categories.Commands.UpdateCategoryNewsCommand;

public sealed class UpdateCategoryNewsCommandHandler(
    Repository<Category> repository,
    ICloudinaryService cloudinary) : ICommandHandler<UpdateCategoryNewsCommand, Result<NewsVm>>
{
    public async Task<Result<NewsVm>> Handle(UpdateCategoryNewsCommand request, CancellationToken cancellationToken)
    {
        var category = await repository.GetByIdAsync(request.NewsRequest.CategoryId, cancellationToken);
        Guard.Against.NotFound(request.NewsRequest.CategoryId, category);

        var news = category.News?.FirstOrDefault(x => x.Id == request.NewsRequest.NewsId);
        Guard.Against.NotFound(request.NewsRequest.NewsId, news);

        if (request.NewsRequest.ImageUrl is { })
        {
            await DeleteNewsImageAsync(news);
            news.Image = request.NewsRequest.ImageUrl;
        }

        if (request.ImageFile is { })
        {
            await DeleteNewsImageAsync(news);
            news.Image = (await cloudinary.AddPhotoAsync(request.ImageFile, "news")).Value.Url;
        }

        news.Update(request.NewsRequest.Title, request.NewsRequest.Detail, news.Image, request.NewsRequest.CategoryId);

        await repository.UpdateAsync(category, cancellationToken);

        return Result<NewsVm>.Success(news.Adapt<NewsVm>());
    }

    private async Task DeleteNewsImageAsync(News news)
    {
        if (news.Image is { }) await cloudinary.DeletePhotoAsync(news.Image);
    }
}