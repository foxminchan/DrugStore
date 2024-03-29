﻿using Ardalis.Result;
using DrugStore.Application.Users.ViewModels;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Users.Queries.GetByIdQuery;

public sealed record GetByIdQuery(IdentityId Id) : IQuery<Result<UserVm>>;