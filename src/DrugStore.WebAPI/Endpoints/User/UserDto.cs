using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Domain.IdentityAggregate.ValueObjects;

namespace DrugStore.WebAPI.Endpoints.User;

public sealed record UserDto(
    IdentityId Id,
    string? Email,
    string? FullName,
    string? Phone,
    Address? Address
);