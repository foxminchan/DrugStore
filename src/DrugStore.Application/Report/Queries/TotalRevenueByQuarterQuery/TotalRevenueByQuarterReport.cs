using Ardalis.Result;
using DrugStore.Application.Abstractions.Queries;
using DrugStore.Application.Report.ViewModels;

namespace DrugStore.Application.Report.Queries.TotalRevenueByQuarterQuery;

public sealed record TotalRevenueByQuarterReport(int Quarter, int Year) : IQuery<Result<List<TotalRevenueByQuarterVm>>>;