using Ardalis.Result;
using DrugStore.Application.Report.ViewModels;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Report.Queries.TopProductByMonthQuery;

public sealed record TopProductsByMonthReport(int Month, int Year, int Limit)
    : IQuery<Result<List<TopProductByMonthVm>>>;