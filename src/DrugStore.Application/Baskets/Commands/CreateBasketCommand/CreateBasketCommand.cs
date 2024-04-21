using Ardalis.Result;
using DrugStore.Domain.BasketAggregate;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Baskets.Commands.CreateBasketCommand;

public sealed record CreateBasketCommand(Guid RequestId, IdentityId CustomerId, BasketItem Item)
    : IdempotencyCommand<Result<IdentityId>>(RequestId);