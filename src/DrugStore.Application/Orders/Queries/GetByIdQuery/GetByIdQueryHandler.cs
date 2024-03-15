using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Application.Orders.ViewModels;
using DrugStore.Domain.OrderAggregate;
using DrugStore.Domain.OrderAggregate.Specifications;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Orders.Queries.GetByIdQuery;

public sealed class GetByIdQueryHandler(IReadRepository<Order> repository)
    : IQueryHandler<GetByIdQuery, Result<OrderDetailVm>>
{
    public async Task<Result<OrderDetailVm>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await repository.FirstOrDefaultAsync(new OrderByIdSpec(request.Id), cancellationToken);
        Guard.Against.NotFound(request.Id, order);

        OrderDetailVm orderVm = new(
            new(
                order.Id,
                order.Code,
                order.Customer,
                order.OrderItems?.Sum(item => item.Quantity * item.Price) ?? 0
            ),
            [
                ..order.OrderItems?.Select(
                    item => new OrderItemVm(
                        item.ProductId,
                        item.OrderId,
                        item.Quantity,
                        item.Price,
                        item.Price * item.Quantity
                    )
                )
            ]
        );

        return Result<OrderDetailVm>.Success(orderVm);
    }
}