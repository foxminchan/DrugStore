using DrugStore.Domain.IdentityAggregate.Primitives;

namespace DrugStore.WebAPI.Endpoints.User;

public sealed class CreateUserResponse(IdentityId id)
{
    public IdentityId Id { get; set; } = id;
}