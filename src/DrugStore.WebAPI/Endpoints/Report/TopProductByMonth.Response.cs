using DrugStore.Application.Report.ViewModels;

namespace DrugStore.WebAPI.Endpoints.Report;

public sealed class TopProductByMonthResponse(List<TopProductByMonthVm> products)
{
    public List<TopProductByMonthVm> Products = products;
}