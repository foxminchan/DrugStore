using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Application.Products.ViewModels;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.SharedKernel;
using DrugStore.Persistence;
using Mapster;

namespace DrugStore.Application.Products.Queries.GetByIdQuery;

public sealed class GetByIdQueryHandler(Repository<Product> repository)
    : IQueryHandler<GetByIdQuery, Result<ProductVm>>
{
    public async Task<Result<ProductVm>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        return Result<ProductVm>.Success(entity.Adapt<ProductVm>() with { Category = entity.Category?.Name });
    }
}