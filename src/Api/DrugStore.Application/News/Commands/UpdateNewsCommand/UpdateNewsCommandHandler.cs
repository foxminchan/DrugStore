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
        await repository.UpdateAsync(news, cancellationToken);

        return Result<NewsVm>.Success(news.Adapt<NewsVm>());
    }

    private async Task DeleteNewsImageAsync(Domain.News.News news)
    {
        if (news.Image is { }) 
            await cloudinary.DeletePhotoAsync(news.Image);
    }
}
