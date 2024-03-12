namespace DrugStore.Domain.IdentityAggregate.Helpers;

public static class ClaimHelper
{
    public const string Read = nameof(Read.ToLowerInvariant);
    public const string Write = nameof(Write.ToLowerInvariant);
    public const string Manage = nameof(Manage.ToLowerInvariant);
}