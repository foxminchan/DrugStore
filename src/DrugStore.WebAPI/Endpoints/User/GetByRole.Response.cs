using Ardalis.Result;

namespace DrugStore.WebAPI.Endpoints.User;

public sealed class GetUserByRoleResponse
{
    public PagedInfo? PagedInfo { get; set; }
    public List<UserDto>? Users { get; set; } = [];
}