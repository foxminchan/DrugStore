using Ardalis.Result;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Orders.Commands.DeleteOrderCommand;

public sealed record DeleteOrderCommand(Guid Id) : ICommand<Result>;