namespace DrugStore.WebAPI.Endpoints.Report;

public sealed class TopProductByMonthRequest(int month, int year, int limit)
{
    public int Month { get; set; } = month;
    public int Year { get; set; } = year;
    public int Limit { get; set; } = limit;
}