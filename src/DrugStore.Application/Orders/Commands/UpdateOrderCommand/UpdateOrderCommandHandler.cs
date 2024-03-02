using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Application.Orders.ViewModels;
using DrugStore.Domain.OrderAggregate;
using DrugStore.Domain.SharedKernel;
using DrugStore.Persistence;

namespace DrugStore.Application.Orders.Commands.UpdateOrderCommand;

public sealed class UpdateOrderCommandHandler(Repository<Order> repository)
    : ICommandHandler<UpdateOrderCommand, Result<OrderVm>>
{
    public async Task<Result<OrderVm>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await repository.GetByIdAsync(request.Request.Id, cancellationToken);
        Guard.Against.NotFound(request.Request.Id, order);
        order.UpdateOrder(
            request.Request.Code,
            request.Request.Status,
            request.Request.PaymentMethod,
            request.Request.CustomerId
        );
        request.Request.Items.ForEach(
            item => order.OrderItems.Add(new(item.Price, item.Quantity, item.Id, request.Request.Id))
        );
        await repository.UpdateAsync(order, cancellationToken);
        return Result<OrderVm>.Success(
            new(
                order.Id,
                order.Code,
                order.Status,
                order.PaymentMethod,
                order.CustomerId,
                order.CreatedDate,
                order.UpdateDate,
                order.Version,
                order.OrderItems.Select(
                    item => new OrderItemVm(
                        item.ProductId,
                        item.OrderId,
                        item.Quantity,
                        item.Price,
                        item.CreatedDate,
                        item.UpdateDate,
                        item.Version
                    )
                ).ToList()
            )
        );
    }
}