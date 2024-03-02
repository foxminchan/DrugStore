using Ardalis.Result;
using DrugStore.Application.Categories.ViewModels;
using DrugStore.Domain.CategoryAggregate.Primitives;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Categories.Queries.GetPostListQuery;

public sealed record GetPostListQuery(CategoryId CategoryId, BaseFilter Filter) : ICommand<PagedResult<List<PostVm>>>;