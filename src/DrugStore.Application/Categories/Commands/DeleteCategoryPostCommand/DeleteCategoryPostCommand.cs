using Ardalis.Result;
using DrugStore.Domain.CategoryAggregate.Primitives;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Categories.Commands.DeleteCategoryPostCommand;

public sealed record DeleteCategoryPostCommand(CategoryId CategoryId, PostId PostId) : ICommand<Result>;