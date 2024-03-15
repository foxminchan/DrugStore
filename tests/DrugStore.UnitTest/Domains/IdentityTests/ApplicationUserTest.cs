using Bogus;
using DrugStore.Domain.IdentityAggregate;
using DrugStore.UnitTest.Builders;
using FluentAssertions;

namespace DrugStore.UnitTest.Domains.IdentityTests;

public sealed class ApplicationUserTest(ITestOutputHelper output)
{
    private readonly Faker _faker = new();

    [Fact]
    public void ShouldBeInitializeApplicationUserSuccessfully()
    {
        // Arrange
        var email = _faker.Person.Email;
        var fullname = _faker.Person.FullName;
        var phoneNumber = _faker.Person.Phone;
        var address = AddressBuilder.WithDefaultValues();

        // Act
        var user = new ApplicationUser(email, fullname, phoneNumber, address);

        // Assert
        user.UserName.Should().Be(email);
        user.PhoneNumber.Should().Be(phoneNumber);
        user.FullName.Should().Be(fullname);
        user.Address.Should().Be(address);
        output.WriteLine("ApplicationUser: {0}", user);
    }

    [Fact]
    public void ShouldBeThrowExceptionWhenEmailIsNull()
    {
        // Arrange
        var email = string.Empty;
        var fullname = _faker.Person.FullName;
        var phoneNumber = _faker.Person.Phone;
        var address = AddressBuilder.WithDefaultValues();

        // Act
        var act = () =>
        {
            var user = new ApplicationUser(email, fullname, phoneNumber, address);
            output.WriteLine("ApplicationUser: {0}", user);
        };

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void ShouldBeThrowExceptionWhenFullNameIsNull()
    {
        // Arrange
        var email = _faker.Person.Email;
        var fullname = string.Empty;
        var phoneNumber = _faker.Person.Phone;
        var address = AddressBuilder.WithDefaultValues();

        // Act
        var act = () =>
        {
            var user = new ApplicationUser(email, fullname, phoneNumber, address);
            output.WriteLine("ApplicationUser: {0}", user);
        };

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void ShouldBeThrowExceptionWhenPhoneNumberIsNull()
    {
        // Arrange
        var email = _faker.Person.Email;
        var fullname = _faker.Person.FullName;
        var phoneNumber = string.Empty;
        var address = AddressBuilder.WithDefaultValues();

        // Act
        var act = () =>
        {
            var user = new ApplicationUser(email, fullname, phoneNumber, address);
            output.WriteLine("ApplicationUser: {0}", user);
        };

        // Assert
        act.Should().Throw<ArgumentException>();
    }
}