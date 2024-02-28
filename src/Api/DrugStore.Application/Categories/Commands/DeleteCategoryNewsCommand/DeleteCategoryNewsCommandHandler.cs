using Ardalis.GuardClauses;
using Ardalis.Result;

using DrugStore.Domain.CategoryAggregate;
using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure.Storage.Cloudinary;
using DrugStore.Persistence;

namespace DrugStore.Application.Categories.Commands.DeleteCategoryNewsCommand;

public sealed class DeleteCategoryNewsCommandHandler(
    Repository<Category> repository,
    ICloudinaryService cloudinary) : ICommandHandler<DeleteCategoryNewsCommand, Result>
{
    public async Task<Result> Handle(DeleteCategoryNewsCommand request, CancellationToken cancellationToken)
    {
        var category = await repository.GetByIdAsync(request.CategoryId, cancellationToken);
        Guard.Against.NotFound(request.CategoryId, category);

        var news = category.News?.FirstOrDefault(x => x.Id == request.NewsId);
        Guard.Against.NotFound(request.NewsId, news);

        if (news.Image is { })
        {
            await cloudinary.DeletePhotoAsync(news.Image);
        }

        category.RemoveNews(news);

        await repository.UpdateAsync(category, cancellationToken);

        return Result.Success();
    }
}
