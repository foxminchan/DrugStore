using Ardalis.Result;
using DrugStore.Application.Users.ViewModels;
using DrugStore.Domain.SharedKernel;
using DrugStore.Persistence.Helpers;

namespace DrugStore.Application.Users.Queries.GetListQuery;

public sealed record GetListQuery(FilterHelper Filter) : IQuery<PagedResult<List<UserVm>>>;