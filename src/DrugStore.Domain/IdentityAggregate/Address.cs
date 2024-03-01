using DrugStore.Domain.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace DrugStore.Domain.IdentityAggregate;

[Owned]
public class Address(string street, string city, string province) : ValueObject
{
    public string Street { get; set; } = street;
    public string City { get; set; } = city;
    public string Province { get; set; } = province;

    public override string ToString() => $"{Street}, {City}, {Province}";

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return ToString();
    }
}