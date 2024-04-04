using Refit;

namespace DrugStore.BackOffice.Components.Pages.Users.Shared.Requests;

public sealed class UpdateUser : CreateUser
{
    [AliasAs("id")] public string? Id { get; set; }

    [AliasAs("OldPassword")] public string? OldPassword { get; set; }
}