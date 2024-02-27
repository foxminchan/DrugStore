using Ardalis.Result;
using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure.Storage.Cloudinary;
using DrugStore.Persistence;

namespace DrugStore.Application.News.Commands.CreateNewsCommand;

public sealed class CreateNewsCommandHandler(
    Repository<Domain.News.News> repository,
    ICloudinaryService cloudinary) : ICommandHandler<CreateNewsCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateNewsCommand request, CancellationToken cancellationToken)
    {
        var news = new Domain.News.News(request.Title, request.Detail, null, request.CategoryId);

        if (request.ImageFile is { })
        {
            var image = await cloudinary.AddPhotoAsync(request.ImageFile, "news");
            news.Image = image.Value.Url;
        }

        await repository.AddAsync(news, cancellationToken);

        return Result<Guid>.Success(news.Id);
    }
}
