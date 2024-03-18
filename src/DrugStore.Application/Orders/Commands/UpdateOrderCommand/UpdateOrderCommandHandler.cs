﻿using Ardalis.GuardClauses;
using Ardalis.Result;
using DrugStore.Application.Orders.ViewModels;
using DrugStore.Domain.OrderAggregate;
using DrugStore.Domain.SharedKernel;
using MapsterMapper;

namespace DrugStore.Application.Orders.Commands.UpdateOrderCommand;

public sealed class UpdateOrderCommandHandler(IMapper mapper, IRepository<Order> repository)
    : ICommandHandler<UpdateOrderCommand, Result<OrderDetailVm>>
{
    public async Task<Result<OrderDetailVm>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await repository.GetByIdAsync(request.Id, cancellationToken);
        Guard.Against.NotFound(request.Id, order);
        order.UpdateOrder(request.Code, request.CustomerId);
        request.Items.ForEach(item => order.OrderItems?.Add(new(item.Price, item.Quantity, item.Id, request.Id)));
        await repository.UpdateAsync(order, cancellationToken);
        return Result<OrderDetailVm>.Success(mapper.Map<OrderDetailVm>(order));
    }
}