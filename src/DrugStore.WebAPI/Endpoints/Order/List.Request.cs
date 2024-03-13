namespace DrugStore.WebAPI.Endpoints.Order;

public sealed class ListOrderRequest(int pageIndex, int pageSize, string? search, string? orderBy, bool isAscending)
{
    public int PageIndex { get; set; } = pageIndex;
    public int PageSize { get; set; } = pageSize;
    public string? Search { get; set; } = search;
    public string? OrderBy { get; set; } = orderBy;
    public bool IsAscending { get; set; } = isAscending;
}