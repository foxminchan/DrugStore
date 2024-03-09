using Ardalis.Result;
using DrugStore.Application.Products.ViewModels;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.ProductAggregate.Specifications;
using DrugStore.Domain.SharedKernel;
using DrugStore.Persistence;
using Mapster;

namespace DrugStore.Application.Products.Queries.GetListByCategoryIdQuery;

public sealed class GetListByCategoryIdQueryHandler(Repository<Product> repository)
    : IQueryHandler<GetListByCategoryIdQuery, PagedResult<List<ProductVm>>>
{
    public async Task<PagedResult<List<ProductVm>>> Handle(GetListByCategoryIdQuery request,
        CancellationToken cancellationToken)
    {
        ProductsByCategoryIdSpec spec = new(request.CategoryId, request.Filter.PageNumber, request.Filter.PageSize);
        var entities = await repository.ListAsync(spec, cancellationToken);
        var totalRecords = await repository.CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling(totalRecords / (double)request.Filter.PageSize);
        PagedInfo pageInfo = new(request.Filter.PageNumber, request.Filter.PageSize, totalPages, totalRecords);
        return new(
            pageInfo, entities.Select(e => e.Adapt<ProductVm>() with { Category = e.Category?.Name }).ToList()
        );
    }
}