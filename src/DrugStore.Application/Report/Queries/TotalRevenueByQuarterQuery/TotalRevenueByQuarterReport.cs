using Ardalis.Result;
using DrugStore.Application.Report.ViewModels;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Report.Queries.TotalRevenueByQuarterQuery;

public sealed record TotalRevenueByQuarterReport(int Quarter, int Year) : IQuery<Result<List<TotalRevenueByQuarterVm>>>;