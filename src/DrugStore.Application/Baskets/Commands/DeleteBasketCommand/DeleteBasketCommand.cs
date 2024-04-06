using Ardalis.Result;
using DrugStore.Application.Abstractions.Commands;
using DrugStore.Domain.IdentityAggregate.Primitives;

namespace DrugStore.Application.Baskets.Commands.DeleteBasketCommand;

public sealed record DeleteBasketCommand(IdentityId BasketId) : ICommand<Result>;