using Ardalis.Result;
using DrugStore.Domain.BasketAggregate;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Baskets.Commands.UpdateBasketCommand;

public sealed record UpdateBasketCommand(Guid CustomerId, BasketItem Item)
    : ICommand<Result<CustomerBasket>>;