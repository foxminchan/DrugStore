namespace DrugStore.BackOffice.Helpers;

public sealed class FilterHelper : PagingHelper
{
    public string? Search { get; set; } = string.Empty;
    public bool IsAscending { get; set; } = true;
    public string? OrderBy { get; set; } = "Id";
}