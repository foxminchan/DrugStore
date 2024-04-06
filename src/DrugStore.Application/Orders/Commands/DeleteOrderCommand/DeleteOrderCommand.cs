using Ardalis.Result;
using DrugStore.Application.Abstractions.Commands;
using DrugStore.Domain.OrderAggregate.Primitives;

namespace DrugStore.Application.Orders.Commands.DeleteOrderCommand;

public sealed record DeleteOrderCommand(OrderId Id) : ICommand<Result>;