using Bogus;
using DrugStore.Domain.IdentityAggregate;

namespace DrugStore.FunctionalTest.Fakers;

public sealed class ApplicationUserFaker : Faker<ApplicationUser>
{
    public ApplicationUserFaker()
    {
        RuleFor(u => u.Email, f => f.Internet.Email());
        RuleFor(u => u.FullName, f => f.Name.FullName());
        RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber("##########"));
        RuleFor(u => u.Address, f => new(
            f.Address.StreetName(), f.Address.City(), f.Address.State())
        );
    }
}