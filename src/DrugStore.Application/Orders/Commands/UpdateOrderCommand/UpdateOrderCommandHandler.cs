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
        var order = await repository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, order);
        order.UpdateOrder(request.Code, request.Status, request.PaymentMethod, request.CustomerId);
        request.Items.ForEach(
            item => order.OrderItems.Add(new(item.Price, item.Quantity, item.Id, request.Id))
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