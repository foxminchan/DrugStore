using Ardalis.GuardClauses;
using DrugStore.Domain.IdentityAggregate.ValueObjects;
using DrugStore.Domain.OrderAggregate;
using DrugStore.Domain.SharedKernel;
using Microsoft.AspNetCore.Identity;

namespace DrugStore.Domain.IdentityAggregate;

public class ApplicationUser : IdentityUser<Guid>, IAggregateRoot
{
    [PersonalData] public virtual string? FullName { get; set; }

    [PersonalData] public virtual Address? Address { get; set; }

    public ICollection<Order> Orders { get; set; } = [];

    public void Update(string? email, string? fullName, string? phone, Address? address)
    {
        Email = Guard.Against.NullOrEmpty(email);
        UserName = Guard.Against.NullOrEmpty(email);
        FullName = Guard.Against.NullOrEmpty(fullName);
        PhoneNumber = Guard.Against.NullOrEmpty(phone);
        Address = address;
    }
}