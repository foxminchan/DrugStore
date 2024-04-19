using Ardalis.Result;
using DrugStore.Application.Abstractions.Queries;
using DrugStore.Application.Report.ViewModels;

namespace DrugStore.Application.Report.Queries.TopProductByMonthQuery;

public sealed record TopProductsByMonthReport(int Month, int Year, int Limit)
    : IQuery<Result<List<TopProductByMonthVm>>>;