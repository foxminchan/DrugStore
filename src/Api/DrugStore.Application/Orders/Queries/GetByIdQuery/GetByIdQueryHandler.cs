using Ardalis.GuardClauses;
using Ardalis.Result;

using DrugStore.Application.Orders.ViewModels;
using DrugStore.Domain.OrderAggregate;
using DrugStore.Domain.SharedKernel;
using DrugStore.Persistence;

namespace DrugStore.Application.Orders.Queries.GetByIdQuery;

public sealed class GetByIdQueryHandler(Repository<Order> repository)
    : IQueryHandler<GetByIdQuery, Result<OrderVm>>
{
    public async Task<Result<OrderVm>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await repository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, order);

        var orderVm = new OrderVm(
            order.Code,
            order.Status,
            order.PaymentMethod,
            order.CustomerId,
            order.CreatedDate,
            order.UpdateDate,
            order.Version);

        return Result<OrderVm>.Success(orderVm);
    }
}
