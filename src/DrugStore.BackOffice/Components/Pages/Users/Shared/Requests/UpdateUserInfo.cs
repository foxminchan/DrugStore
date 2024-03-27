using Refit;

namespace DrugStore.BackOffice.Components.Pages.Users.Shared.Requests;

public sealed class UpdateUserInfo
{
    [AliasAs("id")] public string? Id { get; set; }

    [AliasAs("email")] public string? Email { get; set; }

    [AliasAs("fullName")] public string? FullName { get; set; }

    [AliasAs("phone")] public string? Phone { get; set; }

    [AliasAs("address")] public AddressPayload Address { get; set; } = new();

    [AliasAs("role")] public string? Role { get; set; }
}