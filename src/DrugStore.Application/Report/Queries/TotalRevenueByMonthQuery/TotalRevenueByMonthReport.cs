﻿using Ardalis.Result;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Report.Queries.TotalRevenueByMonthQuery;

public sealed record TotalRevenueByMonthReport(int Month, int Year) : IQuery<Result<decimal>>;