namespace DrugStore.WebAPI.Endpoints.User;

public sealed class ListUserRequest(int pageIndex, int pageSize, string? role, string? search, bool isAscending)
{
    public int PageIndex { get; set; } = pageIndex;
    public int PageSize { get; set; } = pageSize;
    public string? Role { get; set; } = role;
    public string? Search { get; set; } = search;
    public bool IsAscending { get; set; } = isAscending;
}