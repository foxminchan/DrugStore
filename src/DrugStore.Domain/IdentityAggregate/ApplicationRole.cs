using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Domain.SharedKernel;
using Microsoft.AspNetCore.Identity;

namespace DrugStore.Domain.IdentityAggregate;

public sealed class ApplicationRole : IdentityRole<IdentityId>, IAggregateRoot
{
    public ApplicationRole(string name) : base(name) => Id = new(Guid.NewGuid());

    /// <summary>
    ///     EF mapping constructor
    /// </summary>
    public ApplicationRole()
    {
    }
}
