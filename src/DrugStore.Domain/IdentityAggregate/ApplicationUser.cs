using Ardalis.GuardClauses;
using DrugStore.Domain.OrderAggregate;
using Microsoft.AspNetCore.Identity;

namespace DrugStore.Domain.IdentityAggregate;

public class ApplicationUser : IdentityUser<Guid>
{
    [PersonalData] public virtual string? FullName { get; set; }

    [PersonalData] public virtual Address? Address { get; set; }

    public ICollection<Order> Orders { get; set; } = [];

    public void Update(string requestEmail, string? requestFullName, string? requestPhone, Address? requestAddress)
    {
        Email = Guard.Against.NullOrEmpty(requestEmail);
        UserName = Guard.Against.NullOrEmpty(requestEmail);
        FullName = Guard.Against.NullOrEmpty(requestFullName);
        PhoneNumber = Guard.Against.NullOrEmpty(requestPhone);
        Address = requestAddress;
    }
}