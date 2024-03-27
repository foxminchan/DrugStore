using DrugStore.Domain.IdentityAggregate.Constants;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Domain.IdentityAggregate.ValueObjects;

namespace DrugStore.WebAPI.Endpoints.User;

public sealed class UpdateUserInfoRequest
{
    public IdentityId Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? FullName { get; set; }
    public string? Phone { get; set; }
    public Address? Address { get; set; }
    public string Role { get; set; } = Roles.CUSTOMER;
}