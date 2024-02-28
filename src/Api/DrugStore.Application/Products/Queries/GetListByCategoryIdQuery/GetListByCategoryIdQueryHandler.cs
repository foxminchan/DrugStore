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
    public async Task<PagedResult<List<ProductVm>>> Handle(GetListByCategoryIdQuery request, CancellationToken cancellationToken)
    {
        var spec = new ProductsByCategoryIdSpec(
            request.CategoryId,
            request.Filter.PageNumber,
            request.Filter.PageSize,
            request.Filter.IsAscending,
            request.Filter.OrderBy,
            request.Filter.Search
        );

        var entities = await repository.ListAsync(spec, cancellationToken);
        var totalRecords = await repository.CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling(totalRecords / (double)request.Filter.PageSize);
        var pageInfo = new PagedInfo(request.Filter.PageNumber, request.Filter.PageSize, totalPages, totalRecords);
        return new(pageInfo, entities.Adapt<List<ProductVm>>());
    }
}
