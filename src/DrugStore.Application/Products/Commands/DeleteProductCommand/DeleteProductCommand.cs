using Ardalis.Result;
using DrugStore.Application.Abstractions.Commands;
using DrugStore.Domain.ProductAggregate.Primitives;

namespace DrugStore.Application.Products.Commands.DeleteProductCommand;

public sealed record DeleteProductCommand(ProductId Id, bool IsRemoveImage) : ICommand<Result>;