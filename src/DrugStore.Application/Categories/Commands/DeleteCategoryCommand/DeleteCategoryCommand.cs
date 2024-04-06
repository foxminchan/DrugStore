using Ardalis.Result;
using DrugStore.Application.Abstractions.Commands;
using DrugStore.Domain.CategoryAggregate.Primitives;

namespace DrugStore.Application.Categories.Commands.DeleteCategoryCommand;

public sealed record DeleteCategoryCommand(CategoryId Id) : ICommand<Result>;