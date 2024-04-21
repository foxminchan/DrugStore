using System.Text.Json;
using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Application.Abstractions.Commands;
using DrugStore.Application.Orders.ViewModels;
using DrugStore.Domain.OrderAggregate;
using DrugStore.Domain.OrderAggregate.Specifications;
using DrugStore.Persistence.Repositories;
using MapsterMapper;
using Microsoft.Extensions.Logging;

namespace DrugStore.Application.Orders.Commands.UpdateOrderCommand;

public sealed class UpdateOrderCommandHandler(
    IMapper mapper,
    IRepository<Order> repository,
    ILogger<UpdateOrderCommandHandler> logger) : ICommandHandler<UpdateOrderCommand, Result<OrderDetailVm>>
{
    public async Task<Result<OrderDetailVm>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        OrderByIdSpec spec = new(request.Id);
        var order = await repository.GetByIdAsync(spec, cancellationToken);
        Guard.Against.NotFound(request.Id, order);
        order.UpdateOrder(request.Code, request.CustomerId);
        request.Items.ForEach(item => order.OrderItems.Add(new(item.Price, item.Quantity, item.Id, request.Id)));
        logger.LogInformation("[{Command}] Order information: {Order}", nameof(UpdateOrderCommand),
            JsonSerializer.Serialize(order));
        await repository.UpdateAsync(order, cancellationToken);
        return Result<OrderDetailVm>.Success(mapper.Map<OrderDetailVm>(order));
    }
}