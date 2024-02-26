using Ardalis.Result;
using DrugStore.Application.Products.ViewModel;
using DrugStore.Domain.Product;
using DrugStore.Domain.Product.Specifications;
using DrugStore.Domain.SharedKernel;
using DrugStore.Persistence;
using Mapster;

namespace DrugStore.Application.Products.Queries.GetListQuery;

public sealed class GetListQueryHandler(Repository<Product> repository)
    : IQueryHandler<GetListQuery, PagedResult<IEnumerable<ProductVm>>>
{
    public async Task<PagedResult<IEnumerable<ProductVm>>> Handle(GetListQuery request,
        CancellationToken cancellationToken)
    {
        var spec = new ProductsFilterSpec(
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
        return new(pageInfo, entities.Adapt<IEnumerable<ProductVm>>());
    }
}
