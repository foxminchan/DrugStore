using Ardalis.Result;
using DrugStore.Application.Categories.ViewModels;
using DrugStore.Domain.CategoryAggregate.Primitives;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Categories.Queries.GetPostByIdQuery;

public sealed record GetPostByIdQuery(CategoryId CategoryId, PostId PostId) : ICommand<Result<PostVm>>;