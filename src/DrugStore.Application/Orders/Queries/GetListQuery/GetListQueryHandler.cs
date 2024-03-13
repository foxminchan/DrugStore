using Ardalis.Result;
using DrugStore.Application.Orders.ViewModels;
using DrugStore.Domain.OrderAggregate;
using DrugStore.Domain.OrderAggregate.Specifications;
using DrugStore.Domain.SharedKernel;
using DrugStore.Persistence;

namespace DrugStore.Application.Orders.Queries.GetListQuery;

public sealed class GetListQueryHandler(Repository<Order> repository)
    : IQueryHandler<GetListQuery, PagedResult<List<OrderVm>>>
{
    public async Task<PagedResult<List<OrderVm>>> Handle(GetListQuery request,
        CancellationToken cancellationToken)
    {
        OrdersFilterSpec spec = new(
            request.Filter.PageIndex,
            request.Filter.PageSize,
            request.Filter.IsAscending,
            request.Filter.OrderBy,
            request.Filter.Search
        );

        var entities = await repository.ListAsync(spec, cancellationToken);
        var totalRecords = await repository.CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling(totalRecords / (double)request.Filter.PageSize);
        PagedInfo pageInfo = new(
            request.Filter.PageIndex,
            request.Filter.PageSize,
            totalPages,
            totalRecords);

        return new(pageInfo,
        [
            ..entities
                .Select(
                    order => new OrderVm(
                        order.Id,
                        order.Code,
                        order.Customer,
                        order.OrderItems?.Sum(item => item.Quantity * item.Price) ?? 0
                    )
                )
        ]);
    }
}