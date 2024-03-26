namespace DrugStore.WebAPI.Endpoints.User;

public sealed class UpdateUserInfoResponse(UserDto user)
{
    public UserDto User { get; set; } = user;
}