using Ardalis.Result;

using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Categories.Commands.DeleteCategoryPostCommand;

public sealed record DeleteCategoryPostCommand(Guid CategoryId, Guid PostId) : ICommand<Result>;
