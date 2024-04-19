using DrugStore.Application.Report.ViewModels;

namespace DrugStore.WebAPI.Endpoints.Report;

public sealed class TopCustomerByYearResponse(List<TopCustomerByYearVm> customers)
{
    public List<TopCustomerByYearVm> Customers = customers;
}