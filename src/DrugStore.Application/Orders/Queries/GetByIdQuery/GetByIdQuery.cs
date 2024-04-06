using Ardalis.Result;
using DrugStore.Application.Abstractions.Queries;
using DrugStore.Application.Orders.ViewModels;
using DrugStore.Domain.OrderAggregate.Primitives;

namespace DrugStore.Application.Orders.Queries.GetByIdQuery;

public sealed record GetByIdQuery(OrderId Id) : IQuery<Result<OrderDetailVm>>;