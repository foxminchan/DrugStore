using Ardalis.Result;
using DrugStore.Application.Orders.ViewModel;
using DrugStore.Domain.Order.Specifications;
using DrugStore.Domain.Order;
using DrugStore.Domain.SharedKernel;
using DrugStore.Persistence;
using Mapster;

namespace DrugStore.Application.Orders.Queries.GetListByUserIdQuery;

public sealed class GetListByUserIdQueryHandler(Repository<Order> repository)
    : IQueryHandler<GetListByUserIdQuery, PagedResult<IEnumerable<OrderVm>>>
{
    public async Task<PagedResult<IEnumerable<OrderVm>>> Handle(GetListByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        var spec = new OrdersByUserIdSpec(
            request.UserId,
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
        return new(pageInfo, entities.Adapt<IEnumerable<OrderVm>>());
    }
}
