﻿using Ardalis.Result;

using DrugStore.Application.Categories.ViewModel;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Categories.Queries.GetByIdQuery;

public sealed record GetByIdQuery(Guid Id) : IQuery<Result<CategoryVm>>;