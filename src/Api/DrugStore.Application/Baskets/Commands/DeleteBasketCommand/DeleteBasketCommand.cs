using Ardalis.Result;

using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Baskets.Commands.DeleteBasketCommand;

public sealed record DeleteBasketCommand(Guid BasketId) : ICommand<Result>;
