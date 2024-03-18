using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Application.Products.ViewModels;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.ProductAggregate.Specifications;
using DrugStore.Domain.SharedKernel;
using MapsterMapper;

namespace DrugStore.Application.Products.Queries.GetByIdQuery;

public sealed class GetByIdQueryHandler(IMapper mapper, IReadRepository<Product> repository)
    : IQueryHandler<GetByIdQuery, Result<ProductVm>>
{
    public async Task<Result<ProductVm>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.FirstOrDefaultAsync(new ProductByIdSpec(request.Id), cancellationToken);
        Guard.Against.NotFound(request.Id, entity);
        return Result<ProductVm>.Success(mapper.Map<ProductVm>(entity));
    }
}