using Ardalis.Result;
using DrugStore.Application.Orders.ViewModels;
using DrugStore.Domain.OrderAggregate;
using DrugStore.Domain.OrderAggregate.Specifications;
using DrugStore.Domain.SharedKernel;
using DrugStore.Persistence;

namespace DrugStore.Application.Orders.Queries.GetListByUserIdQuery;

public sealed class GetListByUserIdQueryHandler(Repository<Order> repository)
    : IQueryHandler<GetListByUserIdQuery, PagedResult<List<OrderVm>>>
{
    public async Task<PagedResult<List<OrderVm>>> Handle(GetListByUserIdQuery request,
        CancellationToken cancellationToken)
    {
        OrdersByUserIdSpec spec = new(request.UserId, request.Filter.PageNumber, request.Filter.PageSize);

        var entities = await repository.ListAsync(spec, cancellationToken);
        var totalRecords = await repository.CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling(totalRecords / (double)request.Filter.PageSize);
        PagedInfo pageInfo = new(
            request.Filter.PageNumber,
            request.Filter.PageSize,
            totalPages,
            totalRecords
        );

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