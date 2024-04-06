using Ardalis.Result;
using DrugStore.Application.Abstractions.Queries;
using DrugStore.Application.Products.ViewModels;
using DrugStore.Domain.ProductAggregate.Primitives;

namespace DrugStore.Application.Products.Queries.GetByIdQuery;

public sealed record GetByIdQuery(ProductId Id) : IQuery<Result<ProductVm>>;