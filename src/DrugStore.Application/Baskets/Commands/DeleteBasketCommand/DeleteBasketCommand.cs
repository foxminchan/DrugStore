using Ardalis.Result;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Baskets.Commands.DeleteBasketCommand;

public sealed record DeleteBasketCommand(IdentityId BasketId) : ICommand<Result>;