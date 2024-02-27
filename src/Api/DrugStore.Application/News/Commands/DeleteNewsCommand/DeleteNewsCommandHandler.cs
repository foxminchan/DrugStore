using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure.Storage.Cloudinary;
using DrugStore.Persistence;

namespace DrugStore.Application.News.Commands.DeleteNewsCommand;

public sealed class DeleteNewsCommandHandler(
    Repository<Domain.News.News> repository,
    ICloudinaryService cloudinary) : ICommandHandler<DeleteNewsCommand, Result>
{
    public async Task<Result> Handle(DeleteNewsCommand request, CancellationToken cancellationToken)
    {
        var news = await repository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, news);

        if (news.Image is { })
        {
            await cloudinary.DeletePhotoAsync(news.Image);
        }

        await repository.DeleteAsync(news, cancellationToken);
        return Result.Success();
    }
}
