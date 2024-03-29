﻿using Ardalis.Result;
using DrugStore.Application.Orders.ViewModels;
using DrugStore.Domain.SharedKernel;
using DrugStore.Persistence.Helpers;

namespace DrugStore.Application.Orders.Queries.GetListQuery;

public sealed record GetListQuery(FilterHelper Filter) : IQuery<PagedResult<List<OrderVm>>>;