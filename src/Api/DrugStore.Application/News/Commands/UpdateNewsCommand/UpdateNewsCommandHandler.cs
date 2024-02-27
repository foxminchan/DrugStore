using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Application.News.ViewModel;
using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure.Storage.Cloudinary;
using DrugStore.Persistence;
using Mapster;

namespace DrugStore.Application.News.Commands.UpdateNewsCommand;

public sealed class UpdateNewsCommandHandler(
    Repository<Domain.News.News> repository,
    ICloudinaryService cloudinary) : ICommandHandler<UpdateNewsCommand, Result<NewsVm>>
{
    public async Task<Result<NewsVm>> Handle(UpdateNewsCommand request, CancellationToken cancellationToken)
    {
        var news = await repository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, news);

        await DeleteExistingImageAsync(news);

        if (!string.IsNullOrEmpty(request.ImageUrl))
            news.Image = request.ImageUrl;
        else if (request.ImageFile is { })
        {
            var image = await cloudinary.AddPhotoAsync(request.ImageFile, "news");
            news.Image = image.Value.Url;
        }

        news.Update(request.Title, request.Detail, news.Image, request.CategoryId);
        await repository.UpdateAsync(news, cancellationToken);

        return Result<NewsVm>.Success(news.Adapt<NewsVm>());
    }

    private async Task DeleteExistingImageAsync(Domain.News.News news)
    {
        if (!string.IsNullOrEmpty(news.Image))
        {
            await cloudinary.DeletePhotoAsync(news.Image);
        }
    }
}
