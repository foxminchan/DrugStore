using Ardalis.Result;

using DrugStore.Domain.BasketAggregate;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Baskets.Commands.CreateBasketCommand;

public sealed record BasketCreateRequest(Guid CustomerId, BasketItem Item);

public sealed record CreateBasketCommand(Guid RequestId, BasketCreateRequest BasketRequest)
    : IdempotencyCommand<Result<Guid>>(RequestId);
