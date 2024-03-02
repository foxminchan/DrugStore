using Ardalis.Result;
using DrugStore.Domain.OrderAggregate;
using DrugStore.Domain.SharedKernel;
using DrugStore.Persistence;

namespace DrugStore.Application.Orders.Commands.CreateOrderCommand;

public sealed class CreateOrderCommandHandler(Repository<Order> repository)
    : IIdempotencyCommandHandler<CreateOrderCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        Order order = new(
            request.Request.Code,
            request.Request.Status,
            request.Request.PaymentMethod,
            request.Request.CustomerId
        );
        var result = await repository.AddAsync(order, cancellationToken);
        request.Request.Items.ForEach(
            item => order.OrderItems.Add(new(item.Price, item.Quantity, item.Id, result.Id))
        );
        await repository.UpdateAsync(order, cancellationToken);
        order.AddOrder($"user:{request.Request.CustomerId}:cart");
        return Result<Guid>.Success(order.Id);
    }
}