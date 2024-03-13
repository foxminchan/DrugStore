using DrugStore.Domain.IdentityAggregate.ValueObjects;

namespace DrugStore.WebAPI.Endpoints.User;

public sealed class CreateUserRequest(string idempotency, CreateUserPayload user)
{
    public string Idempotency { get; set; } = idempotency;
    public CreateUserPayload User { get; set; } = user;
}

public sealed class CreateUserPayload
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
    public string? FullName { get; set; }
    public string? Phone { get; set; }
    public Address? Address { get; set; }
}