using DrugStore.Domain.IdentityAggregate.Primitives;

namespace DrugStore.WebAPI.Endpoints.User;

public sealed class DeleteUserRequest(IdentityId id)
{
    public IdentityId Id { get; set; } = id;
}