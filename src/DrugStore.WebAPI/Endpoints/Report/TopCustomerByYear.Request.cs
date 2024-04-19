namespace DrugStore.WebAPI.Endpoints.Report;

public sealed class TopCustomerByYearRequest(int year, int limit)
{
    public int Year { get; set; } = year;
    public int Limit { get; set; } = limit;
}