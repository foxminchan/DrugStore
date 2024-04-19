using DrugStore.Application.Report.ViewModels;

namespace DrugStore.WebAPI.Endpoints.Report;

public sealed class TotalRevenueByQuarterResponse(List<TotalRevenueByQuarterVm> revenue)
{
    public List<TotalRevenueByQuarterVm> Revenue { get; set; } = revenue;
}