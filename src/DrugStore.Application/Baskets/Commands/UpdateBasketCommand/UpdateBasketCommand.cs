using Ardalis.Result;
using DrugStore.Application.Abstractions.Commands;
using DrugStore.Application.Baskets.ViewModels;
using DrugStore.Domain.BasketAggregate;
using DrugStore.Domain.IdentityAggregate.Primitives;

namespace DrugStore.Application.Baskets.Commands.UpdateBasketCommand;

public sealed record UpdateBasketCommand(IdentityId CustomerId, BasketItem Item)
    : ICommand<Result<CustomerBasketVm>>;