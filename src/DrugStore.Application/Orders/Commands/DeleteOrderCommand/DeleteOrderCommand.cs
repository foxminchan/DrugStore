using Ardalis.Result;
using DrugStore.Domain.OrderAggregate.Primitives;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Orders.Commands.DeleteOrderCommand;

public sealed record DeleteOrderCommand(OrderId Id) : ICommand<Result>;