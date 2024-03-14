using Refit;

namespace DrugStore.BackOffice.Components.Pages.Users.Shared.Requests;

public class CreateUser
{
    [AliasAs("email")] public string? Email { get; set; }

    [AliasAs("password")] public string? Password { get; set; }

    [AliasAs("confirmPassword")] public string? ConfirmPassword { get; set; }

    [AliasAs("fullName")] public string? FullName { get; set; }

    [AliasAs("phone")] public string? Phone { get; set; }

    [AliasAs("address")] public AddressPayload? Address { get; set; }
}