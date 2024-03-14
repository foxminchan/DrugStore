using Refit;

namespace DrugStore.BackOffice.Components.Pages.Users.Shared.Requests;

public sealed class UpdateUser : CreateUser
{
    [AliasAs("id")] public Guid Id { get; set; }
}