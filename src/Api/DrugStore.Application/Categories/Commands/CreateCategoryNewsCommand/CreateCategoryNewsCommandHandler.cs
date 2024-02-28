﻿using Ardalis.GuardClauses;
using Ardalis.Result;

using DrugStore.Domain.CategoryAggregate;
using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure.Storage.Cloudinary;
using DrugStore.Persistence;

namespace DrugStore.Application.Categories.Commands.CreateCategoryNewsCommand;

public sealed class CreateCategoryNewsCommandHandler(
    Repository<Category> repository,
    ICloudinaryService cloudinary) : ICommandHandler<CreateCategoryNewsCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateCategoryNewsCommand request, CancellationToken cancellationToken)
    {
        var category = await repository.GetByIdAsync(request.CategoryId, cancellationToken);
        Guard.Against.NotFound(request.CategoryId, category);

        var news = new News(request.Title, request.Detail, null, request.CategoryId);

        if (request.ImageFile is { })
        {
            var image = await cloudinary.AddPhotoAsync(request.ImageFile, "news");
            news.Image = image.Value.Url;
        }

        category.AddNews(news);

        await repository.UpdateAsync(category, cancellationToken);

        return Result<Guid>.Success(news.Id);
    }
}