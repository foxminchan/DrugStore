namespace DrugStore.WebAPI.Endpoints.User;

public sealed class UpdateUserResponse(UserDto user)
{
    public UserDto User { get; set; } = user;
}