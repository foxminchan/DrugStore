using DrugStore.Domain.IdentityAggregate.Primitives;

namespace DrugStore.WebAPI.Endpoints.User;

public sealed class ResetPasswordResponse(IdentityId id, string password = "P@ssw0rd")
{
    public IdentityId Id { get; set; } = id;
    public string Password { get; set; } = password;
}