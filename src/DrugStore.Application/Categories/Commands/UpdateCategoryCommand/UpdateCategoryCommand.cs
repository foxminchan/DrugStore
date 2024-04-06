using Ardalis.Result;
using DrugStore.Application.Abstractions.Commands;
using DrugStore.Application.Categories.ViewModels;
using DrugStore.Domain.CategoryAggregate.Primitives;

namespace DrugStore.Application.Categories.Commands.UpdateCategoryCommand;

public sealed record UpdateCategoryCommand(CategoryId Id, string Name, string? Description)
    : ICommand<Result<CategoryVm>>;