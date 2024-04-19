namespace DrugStore.WebAPI.Endpoints.Report;

public sealed class DiffRevenueByMonthRequest(int sourceMonth, int sourceYear, int targetMonth, int targetYear)
{
    public int SourceMonth { get; set; } = sourceMonth;
    public int SourceYear { get; set; } = sourceYear;
    public int TargetMonth { get; set; } = targetMonth;
    public int TargetYear { get; set; } = targetYear;
}