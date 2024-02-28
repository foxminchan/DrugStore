using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Application.News.ViewModel;
using DrugStore.Domain.News.Specifications;
using DrugStore.Domain.SharedKernel;
using DrugStore.Persistence;
using Mapster;

namespace DrugStore.Application.News.Queries.GetByIdQuery;

public sealed class GetByIdQueryHandler(Repository<Domain.News.News> repository)
    : IQueryHandler<GetByIdQuery, Result<NewsVm>>
{
    public async Task<Result<NewsVm>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.FirstOrDefaultAsync(new NewsByIdSpec(request.Id), cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        return Result<NewsVm>.Success(entity.Adapt<NewsVm>());
    }
}
