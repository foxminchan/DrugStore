using Ardalis.Result;
using DrugStore.Application.Users.ViewModels;
using DrugStore.Domain.SharedKernel;
using DrugStore.Persistence.Helpers;

namespace DrugStore.Application.Users.Queries.GetByRoleQuery;

public sealed record GetByRoleQuery(FilterHelper Filter, bool IsStaff = true) : IQuery<PagedResult<List<UserVm>>>;