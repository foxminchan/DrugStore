namespace DrugStore.BackOffice.Helpers;

public sealed class FilterHelper : PagingHelper
{
    public string? Search { get; set; } = string.Empty;
    public string? OrderBy { get; set; } = "Id";
    public bool IsAscending { get; set; } = true;
}