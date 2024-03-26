using DrugStore.Domain.IdentityAggregate.Primitives;

namespace DrugStore.WebAPI.Endpoints.User;

public sealed class ResetPasswordRequest(IdentityId id)
{
    public IdentityId Id { get; set; } = id;
}