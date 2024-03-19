using Ardalis.GuardClauses;
using DrugStore.Domain.IdentityAggregate.Primitives;
using DrugStore.Domain.IdentityAggregate.ValueObjects;
using DrugStore.Domain.OrderAggregate;
using DrugStore.Domain.SharedKernel;
using Microsoft.AspNetCore.Identity;

namespace DrugStore.Domain.IdentityAggregate;

public sealed class ApplicationUser : IdentityUser<IdentityId>, IAggregateRoot
{
    /// <summary>
    ///     EF mapping constructor
    /// </summary>
    public ApplicationUser()
    {
    }

    public ApplicationUser(string email, string? fullName, string? phoneNumber, Address? address)
        : base(email)
    {
        Id = new(Guid.NewGuid());
        Email = Guard.Against.NullOrEmpty(email);
        UserName = Guard.Against.NullOrEmpty(email);
        FullName = Guard.Against.NullOrEmpty(fullName);
        PhoneNumber = Guard.Against.NullOrEmpty(phoneNumber);
        Address = address;
    }

    [PersonalData] public string? FullName { get; set; }
    [PersonalData] public Address? Address { get; set; }
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