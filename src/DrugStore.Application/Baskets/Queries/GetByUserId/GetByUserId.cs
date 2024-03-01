using Ardalis.Result;
using DrugStore.Application.Baskets.ViewModels;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Baskets.Queries.GetByUserId;

public sealed record GetByUserId(Guid CustomerId, BaseFilter Filter) : IQuery<Result<BasketVm>>;