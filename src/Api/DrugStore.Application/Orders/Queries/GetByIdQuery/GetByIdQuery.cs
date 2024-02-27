using Ardalis.Result;
using DrugStore.Application.Orders.ViewModel;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Orders.Queries.GetByIdQuery;

public sealed record GetByIdQuery(Guid Id) : IQuery<Result<OrderVm>>;
