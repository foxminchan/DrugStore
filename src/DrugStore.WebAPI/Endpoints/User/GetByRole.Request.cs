namespace DrugStore.WebAPI.Endpoints.User;

public sealed class GetUserByRoleRequest(bool isStaff, int pageIndex, int pageSize, string? search, bool isAscending)
{
    public bool IsStaff { get; set; } = isStaff;
    public int PageIndex { get; set; } = pageIndex;
    public int PageSize { get; set; } = pageSize;
    public string? Search { get; set; } = search;
    public bool IsAscending { get; set; } = isAscending;
}