using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Application.Orders.ViewModels;
using DrugStore.Domain.OrderAggregate;
using DrugStore.Domain.OrderAggregate.Specifications;
using DrugStore.Domain.SharedKernel;
using DrugStore.Persistence;

namespace DrugStore.Application.Orders.Queries.GetByIdQuery;

public sealed class GetByIdQueryHandler(Repository<Order> repository)
    : IQueryHandler<GetByIdQuery, Result<OrderVm>>
{
    public async Task<Result<OrderVm>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await repository.FirstOrDefaultAsync(new OrderByIdSpec(request.Id), cancellationToken);
        Guard.Against.NotFound(request.Id, order);

        OrderVm orderVm = new(
            order.Id,
            order.Code,
            order.CustomerId,
            order.OrderItems.Select(
                item => new OrderItemVm(
                    item.ProductId,
                    item.OrderId,
                    item.Quantity,
                    item.Price
                )
            ).ToList()
        );

        return Result<OrderVm>.Success(orderVm);
    }
}