using Ardalis.Result;
using DrugStore.Domain.CategoryAggregate.Primitives;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Categories.Commands.DeleteCategoryNewsCommand;

public sealed record DeleteCategoryNewsCommand(CategoryId CategoryId, NewsId NewsId) : ICommand<Result>;