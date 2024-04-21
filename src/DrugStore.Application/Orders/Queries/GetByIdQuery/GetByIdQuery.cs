using Ardalis.Result;
using DrugStore.Application.Orders.ViewModels;
using DrugStore.Domain.OrderAggregate.Primitives;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Orders.Queries.GetByIdQuery;

public sealed record GetByIdQuery(OrderId Id) : IQuery<Result<OrderDetailVm>>;