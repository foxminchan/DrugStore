using Ardalis.Result;
using DrugStore.Application.Products.ViewModel;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Products.Queries.GetByIdQuery;

public sealed record GetByIdQuery(Guid Id) : IQuery<Result<ProductVm>>;
