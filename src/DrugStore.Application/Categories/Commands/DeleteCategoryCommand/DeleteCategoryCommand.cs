using Ardalis.Result;
using DrugStore.Domain.CategoryAggregate.Primitives;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Categories.Commands.DeleteCategoryCommand;

public sealed record DeleteCategoryCommand(CategoryId Id) : ICommand<Result>;