using Ardalis.Result;

using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Categories.Commands.CreateCategoryCommand;

public sealed record CreateCategoryCommand(string Title, string? Link) : ICommand<Result<Guid>>;
