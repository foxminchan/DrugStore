using DrugStore.Domain.ProductAggregate.ValueObjects;
using FluentAssertions;

namespace DrugStore.UnitTest.Domains.ProductTests.ValueObjects;

public sealed class ProductPriceTest
{
    private static readonly ProductPrice _price = new(15.7m, 8.6m);

    public static readonly TheoryData<ProductPrice?, ProductPrice?, string> EqualPriceData = new()
    {
        {
            _price,
            _price,
            "Two prices are equal because they are the same object"
        },
        {
            new(15.7m, 8.6m),
            _price,
            "Two prices are equal because they have the same properties"
        },
        {
            null,
            null,
            "Two prices are equal because they are null"
        }
    };

    public static readonly TheoryData<ProductPrice?, ProductPrice?, string> NonEqualPriceData = new()
    {
        {
            new(15.7m, 8.6m),
            new(15.7m, 18.6m),
            "Two prices are non-equal because they are the same object"
        },
        {
            new(15.7m, 84.6m),
            _price,
            "Two prices are non-equal because they have different properties"
        },
        {
            new(15.7m, 8.6m),
            null,
            "Two prices are non-equal because one of them is null"
        }
    };

    [Fact]
    public void ShouldBeInitializeProductPriceSuccessfully()
    {
        // Arrange
        const decimal price = 15.7m;
        const decimal priceSale = 8.6m;

        // Act
        var productPrice = new ProductPrice(price, priceSale);

        // Assert
        productPrice.Price.Should().Be(price);
        productPrice.PriceSale.Should().Be(priceSale);
    }

    [Fact]
    public void ShouldBeThrowExceptionWhenInitializeProductPriceWithNegativePrice()
    {
        // Arrange
        const decimal price = -15.7m;
        const decimal priceSale = 8.6m;

        // Act
        var act = () => new ProductPrice(price, priceSale);

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void ShouldBeThrowExceptionWhenInitializeProductPriceWithNegativePriceSale()
    {
        // Arrange
        const decimal price = 15.7m;
        const decimal priceSale = -8.6m;

        // Act
        var act = () => new ProductPrice(price, priceSale);

        // Assert
        act.Should().NotThrow();
    }

    [Theory]
    [MemberData(nameof(EqualPriceData))]
    public void ShouldBeEqualPrice(ProductPrice? price1, ProductPrice? price2, string reason)
    {
        // Act
        var result = EqualityComparer<ProductPrice>.Default.Equals(price1, price2);

        // Assert
        Assert.True(result, reason);
    }

    [Theory]
    [MemberData(nameof(NonEqualPriceData))]
    public void ShouldBeNonEqualPrice(ProductPrice? price1, ProductPrice? price2, string reason)
    {
        // Act
        var result = EqualityComparer<ProductPrice>.Default.Equals(price1, price2);

        // Assert
        Assert.False(result, reason);
    }
}