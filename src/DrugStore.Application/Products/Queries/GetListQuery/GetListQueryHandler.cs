using Ardalis.Result;
using DrugStore.Application.Products.ViewModels;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.ProductAggregate.Specifications;
using DrugStore.Domain.SharedKernel;
using DrugStore.Persistence;
using Mapster;

namespace DrugStore.Application.Products.Queries.GetListQuery;

public sealed class GetListQueryHandler(Repository<Product> repository)
    : IQueryHandler<GetListQuery, PagedResult<List<ProductVm>>>
{
    public async Task<PagedResult<List<ProductVm>>> Handle(GetListQuery request,
        CancellationToken cancellationToken)
    {
        ProductsFilterSpec spec = new(
            request.Filter.Paging.PageNumber,
            request.Filter.Paging.PageSize,
            request.Filter.IsAscending,
            request.Filter.OrderBy,
            request.Filter.Search
        );

        var entities = await repository.ListAsync(spec, cancellationToken);
        var totalRecords = await repository.CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling(totalRecords / (double)request.Filter.Paging.PageSize);
        PagedInfo pageInfo = new(
            request.Filter.Paging.PageNumber,
            request.Filter.Paging.PageSize,
            totalPages,
            totalRecords
        );
        return new(pageInfo, entities.Adapt<List<ProductVm>>());
    }
}