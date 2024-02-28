﻿using Ardalis.Result;

using DrugStore.Application.Users.ViewModels;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Users.Queries.GetListQuery;

public sealed record GetListQuery(BaseFilter Filter) : IQuery<PagedResult<List<UserVm>>>;