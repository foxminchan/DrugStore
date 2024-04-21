using Ardalis.Result;
using DrugStore.Application.Report.ViewModels;
using DrugStore.Domain.SharedKernel;

namespace DrugStore.Application.Report.Queries.TopCustomerByYearQuery;

public sealed record TopCustomerByYearReport(int Year, int Limit) : IQuery<Result<List<TopCustomerByYearVm>>>;