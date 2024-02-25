using Ardalis.Result;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Products.Commands.DeleteProductCommand;

public sealed record DeleteProductCommand(Guid Id) : ICommand<Result>;
