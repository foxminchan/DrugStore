using DrugStore.Domain.SharedKernel;
using Microsoft.AspNetCore.Identity;

namespace DrugStore.Domain.IdentityAggregate;

public class ApplicationRole : IdentityRole<Guid>, IAggregateRoot
{
    public ApplicationRole(string name) : base(name)
    {
    }

    /// <summary>
    ///     EF mapping constructor
    /// </summary>
    public ApplicationRole()
    {
    }
}