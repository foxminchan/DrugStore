using Ardalis.Result;
using DrugStore.Application.Abstractions.Queries;
using DrugStore.Application.Report.ViewModels;

namespace DrugStore.Application.Report.Queries.DiffRevenueByMonthQuery;

public sealed record DiffRevenueByMonthReport(
    int SourceMonth,
    int SourceYear,
    int TargetMonth,
    int TargetYear) : IQuery<Result<DiffRevenueByMonthVm>>;