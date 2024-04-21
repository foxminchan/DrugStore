using Ardalis.Result;
using DrugStore.Application.Products.ViewModels;
using DrugStore.Domain.ProductAggregate.Primitives;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Products.Queries.GetByIdQuery;

public sealed record GetByIdQuery(ProductId Id) : IQuery<Result<ProductVm>>;