using Refit;

namespace DrugStore.BackOffice.Components.Pages.Users.Shared.Requests;

public sealed class AddressPayload
{
    [AliasAs("street")] public string? Street { get; set; }

    [AliasAs("city")] public string? City { get; set; }

    [AliasAs("province")] public string? Province { get; set; }
}