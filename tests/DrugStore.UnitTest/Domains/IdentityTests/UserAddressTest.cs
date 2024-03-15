﻿using DrugStore.Domain.IdentityAggregate.ValueObjects;
using FluentAssertions;

namespace DrugStore.UnitTest.Domains.IdentityTests;

public sealed class UserAddressTest
{
    [Fact]
    public void ShouldBeInitializeUserAddressSuccessfully()
    {
        // Arrange
        const string street = "Nam Ky Khoi Nghia";
        const string city = "District 3";
        const string province = "Ho Chi Minh";

        // Act
        var address = new Address(street, city, province);

        // Assert
        address.Street.Should().Be(street);
        address.City.Should().Be(city);
        address.Province.Should().Be(province);
    }

    [Theory]
    [MemberData(nameof(EqualAddressData))]
    public void ShouldBeEqualAddress(Address? address1, Address? address2, string reason)
    {
        // Act
        var result = EqualityComparer<Address>.Default.Equals(address1, address2);

        // Assert
        Assert.True(result, reason);
    }

    [Theory]
    [MemberData(nameof(NonEqualAddressData))]
    public void ShouldBeNonEqualAddress(Address? address1, Address? address2, string reason)
    {
        // Act
        var result = EqualityComparer<Address>.Default.Equals(address1, address2);

        // Assert
        Assert.False(result, reason);
    }

    private static readonly Address Address = new("Nam Ky Khoi Nghia", "District 3", "Ho Chi Minh");

    public static readonly TheoryData<Address?, Address?, string> EqualAddressData = new()
    {
        {
            Address,
            Address,
            "Two addresses are equal because they are the same object"
        },
        {
            new("Nam Ky Khoi Nghia", "District 3", "Ho Chi Minh"),
            Address,
            "Two addresses are equal because they have the same properties"
        },
        {
            new("Nam Ky Khoi Nghia", "District 3", "Ho Chi Minh"),
            new("Nam Ky Khoi Nghia", "District 3", "Ho Chi Minh"),
            "Two addresses are equal because they have the same properties"
        },
        {
            null,
            null,
            "Two null addresses are equal because they are both null"
        }
    };

    public static readonly TheoryData<Address?, Address?, string> NonEqualAddressData = new()
    {
        {
            new("Nam Ky Khoi Nghia", "District 3", "Ho Chi Minh"),
            new("Xa Lo Ha Noi", "Thu Duc", "Ho Chi Minh"),
            "Two addresses are not equal because they have different properties"
        },
        {
            Address,
            new("Phan Van Tri", "Go Vap", "Ho Chi Minh"),
            "Two addresses are not equal because they have different properties"
        },
        {
            Address,
            null,
            "An address is not equal to null"
        },
        {
            null,
            Address,
            "An address is not equal to null"
        }
    };
}