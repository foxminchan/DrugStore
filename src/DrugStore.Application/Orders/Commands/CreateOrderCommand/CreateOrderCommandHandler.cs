using System.Text.Json;
using Ardalis.Result;
using DrugStore.Domain.OrderAggregate;
using DrugStore.Domain.OrderAggregate.Primitives;
using DrugStore.Domain.SharedKernel;
using DrugStore.Persistence.Repositories;
using Microsoft.Extensions.Logging;

namespace DrugStore.Application.Orders.Commands.CreateOrderCommand;

public sealed class CreateOrderCommandHandler(IRepository<Order> repository, ILogger<CreateOrderCommandHandler> logger)
    : IIdempotencyCommandHandler<CreateOrderCommand, Result<OrderId>>
{
    public async Task<Result<OrderId>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = Order.Factory.Create(
            request.CustomerId,
            request.Code,
            request.Items.Select(x => new OrderItem(x.Price, x.Quantity, x.Id))
        );

        logger.LogInformation("[{Command}] Order information: {Request}", nameof(CreateOrderCommand),
            JsonSerializer.Serialize(request));

        await repository.AddAsync(order, cancellationToken);

        order.AddOrder($"user:{request.CustomerId}:cart");

        return Result<OrderId>.Success(order.Id);
    }
}