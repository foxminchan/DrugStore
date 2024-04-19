namespace DrugStore.Application.Report.ViewModels;

public sealed record TopCustomerByYearVm(
    string FullName,
    decimal TotalAmount
);