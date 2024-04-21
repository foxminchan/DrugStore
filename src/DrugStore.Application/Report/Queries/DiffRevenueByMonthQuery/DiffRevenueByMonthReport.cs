using Ardalis.Result;
using DrugStore.Application.Report.ViewModels;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Report.Queries.DiffRevenueByMonthQuery;

public sealed record DiffRevenueByMonthReport(
    int SourceMonth,
    int SourceYear,
    int TargetMonth,
    int TargetYear) : IQuery<Result<DiffRevenueByMonthVm>>;