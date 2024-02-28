using Ardalis.Result;

using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Categories.Commands.DeleteCategoryNewsCommand;

public sealed record DeleteCategoryNewsCommand(Guid CategoryId, Guid NewsId) : ICommand<Result>;
