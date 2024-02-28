using Ardalis.Result;

using DrugStore.Application.Products.ViewModels;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Products.Queries.GetByIdQuery;

public sealed record GetByIdQuery(Guid Id) : IQuery<Result<ProductVm>>;
