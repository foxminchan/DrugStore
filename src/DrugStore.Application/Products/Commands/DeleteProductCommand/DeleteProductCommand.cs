using Ardalis.Result;
using DrugStore.Domain.ProductAggregate.Primitives;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Products.Commands.DeleteProductCommand;

public sealed record DeleteProductCommand(ProductId Id, bool IsRemoveImage) : ICommand<Result>;