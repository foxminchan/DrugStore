namespace DrugStore.WebAPI.Endpoints.Report;

public sealed class TotalRevenueByQuarterRequest(int quarter, int year)
{
    public int Quarter { get; init; } = quarter;
    public int Year { get; init; } = year;
}