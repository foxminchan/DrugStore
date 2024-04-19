namespace DrugStore.Application.Report.ViewModels;

public sealed record DiffRevenueByMonthVm(
    string SourceMonthYear,
    string TargetMonthYear,
    decimal Diff
);