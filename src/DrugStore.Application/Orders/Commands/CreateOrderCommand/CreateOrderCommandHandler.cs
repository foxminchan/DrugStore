using System.Text.Json;
using Ardalis.Result;
using DrugStore.Domain.OrderAggregate;
using DrugStore.Domain.OrderAggregate.Primitives;
using DrugStore.Domain.SharedKernel;
using Microsoft.Extensions.Logging;

namespace DrugStore.Application.Orders.Commands.CreateOrderCommand;

public sealed class CreateOrderCommandHandler(IRepository<Order> repository, ILogger<CreateOrderCommandHandler> logger)
    : IIdempotencyCommandHandler<CreateOrderCommand, Result<OrderId>>
{
    public async Task<Result<OrderId>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        Order order = new(request.Code, request.CustomerId);
        var result = await repository.AddAsync(order, cancellationToken);
        request.Items.ForEach(
            item => order.OrderItems.Add(new(item.Price, item.Quantity, item.Id, result.Id))
        );
        logger.LogInformation("[{Command}] Order information: {Order}", nameof(CreateOrderCommand),
            JsonSerializer.Serialize(order));
        await repository.UpdateAsync(order, cancellationToken);
        order.AddOrder($"user:{request.CustomerId}:cart");
        return Result<OrderId>.Success(order.Id);
    }
}