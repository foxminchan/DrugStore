using Ardalis.Result;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Categories.Commands.DeleteCategoryCommand;

public sealed record DeleteCategoryCommand(Guid Id) : ICommand<Result>;
