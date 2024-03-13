using DrugStore.Domain.IdentityAggregate;
using DrugStore.Domain.OrderAggregate.Primitives;

namespace DrugStore.Application.Orders.ViewModels;

public sealed record OrderVm(
    OrderId Id,
    string? Code,
    ApplicationUser? Customer,
    decimal Total
);