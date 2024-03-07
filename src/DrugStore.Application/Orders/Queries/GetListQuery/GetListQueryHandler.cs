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
            request.Filter.PageNumber,
            request.Filter.PageSize,
            request.Filter.IsAscending,
            request.Filter.OrderBy,
            request.Filter.Search
        );

        var entities = await repository.ListAsync(spec, cancellationToken);
        var totalRecords = await repository.CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling(totalRecords / (double)request.Filter.PageSize);
        PagedInfo pageInfo = new(
            request.Filter.PageNumber,
            request.Filter.PageSize,
            totalPages,
            totalRecords);

        var orderVms = entities
            .Select(
                order => new OrderVm(
                    order.Id,
                    order.Code,
                    order.Status,
                    order.PaymentMethod,
                    order.CustomerId,
                    order.OrderItems.Select(
                        item => new OrderItemVm(
                            item.ProductId,
                            item.OrderId,
                            item.Quantity,
                            item.Price
                        )
                    ).ToList()
                )
            ).ToList();

        return new(pageInfo, orderVms);
    }
}