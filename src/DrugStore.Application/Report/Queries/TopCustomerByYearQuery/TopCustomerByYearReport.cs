using Ardalis.Result;
using DrugStore.Application.Abstractions.Queries;
using DrugStore.Application.Report.ViewModels;

namespace DrugStore.Application.Report.Queries.TopCustomerByYearQuery;

public sealed record TopCustomerByYearReport(int Year, int Limit) : IQuery<Result<List<TopCustomerByYearVm>>>;