using Ardalis.Result;

namespace DrugStore.WebAPI.Endpoints.User;

public sealed class ListUserResponse
{
    public PagedInfo? PagedInfo { get; set; }
    public List<UserDto>? Users { get; set; } = [];
}