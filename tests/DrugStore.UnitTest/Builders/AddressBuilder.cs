using DrugStore.Domain.IdentityAggregate.ValueObjects;

namespace DrugStore.UnitTest.Builders;

public sealed class AddressBuilder
{
    public readonly Address Address = WithDefaultValues();
    public static string Street => "Nam Ky Khoi Nghia";
    public static string City => "District 1";
    public static string Province => "Ho Chi Minh";

    public Address Build() => Address;

    public static Address WithDefaultValues() => new(Street, City, Province);
}