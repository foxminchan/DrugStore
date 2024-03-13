using Ardalis.Result;
using DrugStore.Application.Orders.ViewModels;
using DrugStore.Domain.OrderAggregate;
using DrugStore.Domain.OrderAggregate.Specifications;
using DrugStore.Domain.SharedKernel;
using DrugStore.Persistence;

namespace DrugStore.Application.Orders.Queries.GetListByUserIdQuery;

public sealed class GetListByUserIdQueryHandler(Repository<Order> repository)
    : IQueryHandler<GetListByUserIdQuery, PagedResult<List<OrderDetailVm>>>
{
    public async Task<PagedResult<List<OrderDetailVm>>> Handle(GetListByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        OrdersByUserIdSpec spec = new(request.UserId, request.Filter.PageIndex, request.Filter.PageSize);

        var entities = await repository.ListAsync(spec, cancellationToken);
        var totalRecords = await repository.CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling(totalRecords / (double)request.Filter.PageSize);
        PagedInfo pageInfo = new(
            request.Filter.PageIndex,
            request.Filter.PageSize,
            totalPages,
            totalRecords
        );

        var orderVms = entities
            .Select(
                order => new OrderDetailVm(
                    new(
                        order.Id,
                        order.Code,
                        order.Customer,
                        order.OrderItems?.Sum(item => item.Quantity * item.Price) ?? 0
                    ),
                    [
                        .. order.OrderItems?.Select(
                            item => new OrderItemVm(
                                item.ProductId,
                                item.OrderId,
                                item.Quantity,
                                item.Price,
                                item.Price * item.Quantity
                            )
                        )
                    ]
                )
            ).ToList();

        return new(pageInfo, orderVms);
    }
}