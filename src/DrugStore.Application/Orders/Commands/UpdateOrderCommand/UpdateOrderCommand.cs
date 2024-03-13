using Ardalis.Result;
using DrugStore.Application.Orders.ViewModels;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Domain.OrderAggregate.Primitives;
using DrugStore.Domain.ProductAggregate.Primitives;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Orders.Commands.UpdateOrderCommand;

public sealed record OrderItemUpdateRequest(ProductId Id, int Quantity, decimal Price);

public sealed record UpdateOrderCommand(
    OrderId Id,
    string? Code,
    IdentityId? CustomerId,
    List<OrderItemUpdateRequest> Items) : ICommand<Result<OrderDetailVm>>;