namespace DrugStore.WebAPI.Endpoints.Report;

public sealed class TotalRevenueByMonthRequest(int month, int year)
{
    public int Month { get; init; } = month;
    public int Year { get; init; } = year;
}