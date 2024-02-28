using DrugStore.Domain.OrderAggregate;

using Microsoft.AspNetCore.Identity;

namespace DrugStore.Domain.IdentityAggregate;

public class ApplicationUser : IdentityUser<Guid>
{
    [PersonalData]
    public virtual string? FullName { get; set; }

    [PersonalData]
    public virtual Address? Address { get; set; }

    public ICollection<Order> Orders { get; set; } = [];
}
