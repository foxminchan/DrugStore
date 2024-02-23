using Microsoft.AspNetCore.Identity;

namespace DrugStore.Domain.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    [PersonalData]
    public virtual string? FullName { get; set; }

    [PersonalData]
    public virtual string? Phone { get; set; }

    [PersonalData]
    public virtual Address? Address { get; set; }

    public ICollection<Order.Order> Orders { get; set; } = [];
}
