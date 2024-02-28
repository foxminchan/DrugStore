﻿using Ardalis.GuardClauses;
using Ardalis.Result;

using DrugStore.Application.Categories.ViewModels;
using DrugStore.Domain.CategoryAggregate;
using DrugStore.Domain.CategoryAggregate.Specifications;
using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure.Cache.Redis;
using DrugStore.Persistence;

using Mapster;

namespace DrugStore.Application.Categories.Queries.GetByIdQuery;

public sealed class GetByIdQueryHandler(Repository<Category> repository, IRedisService redisService)
    : IQueryHandler<GetByIdQuery, Result<CategoryVm>>
{
    public async Task<Result<CategoryVm>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var categories = redisService.Get<IEnumerable<CategoryVm>>("categories");

        if (categories is { })
        {
            var category = categories.FirstOrDefault(c => c.Id == request.Id);
            Guard.Against.NotFound(request.Id, category);
            return Result<CategoryVm>.Success(category);
        }

        var entity = await repository.FirstOrDefaultAsync(new CategoryByIdSpec(request.Id), cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        return Result<CategoryVm>.Success(entity.Adapt<CategoryVm>());
    }
}
