using Ardalis.Result;

using DrugStore.Application.Categories.ViewModels;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Categories.Commands.UpdateCategoryCommand;

public record UpdateCategoryCommand(Guid Id, string Title, string? Link) : ICommand<Result<CategoryVm>>;
