using DrugStore.Domain.ProductAggregate.ValueObjects;
using FluentAssertions;

namespace DrugStore.UnitTest.Domains.ProductTests.ValueObjects;

public sealed class ProductImageTest
{
    private static readonly ProductImage _productImage = new(Guid.Empty.ToString(), "Mock Image", "Mock Title");

    public static readonly TheoryData<ProductImage?, ProductImage?, string> EqualImage = new()
    {
        {
            _productImage,
            _productImage,
            "Two images are equal because they are the same object"
        },
        {
            null,
            null,
            "Two images are equal because they are null"
        },
        {
            new(Guid.Empty.ToString(), "Mock Image", "Mock Title"),
            new(Guid.Empty.ToString(), "Mock Image", "Mock Title"),
            "Two images are equal because they have the same properties"
        }
    };

    public static readonly TheoryData<ProductImage?, ProductImage?, string> NonEqualImage = new()
    {
        {
            _productImage,
            new(Guid.NewGuid().ToString(), "Mock Image", "Mock Title"),
            "Two images are non-equal because they are different objects"
        },
        {
            new(Guid.NewGuid().ToString(), "Mock Image", "Mock Title"),
            new(Guid.Empty.ToString(), "Mock Image", "Mock Title"),
            "Two images are non-equal because they have different properties"
        },
        {
            new(Guid.Empty.ToString(), "Mock Image", "Mock Title"),
            null,
            "Two images are non-equal because they are different objects"
        }
    };

    [Fact]
    public void ShouldBeInitializeProductImageSuccessfully()
    {
        // Arrange
        var imageUrl = Guid.NewGuid().ToString();
        const string alt = "Mock Image";
        const string title = "Mock Title";

        // Act
        var productImage = new ProductImage(imageUrl, alt, title);

        // Assert
        Assert.Equal(imageUrl, productImage.ImageUrl);
        Assert.Equal(alt, productImage.Alt);
        Assert.Equal(title, productImage.Title);
    }

    [Fact]
    public void ShouldBeThrowExceptionWhenInitializeProductImageWithEmptyImageUrl()
    {
        // Arrange
        const string imageUrl = "";
        const string alt = "Mock Image";
        const string title = "Mock Title";

        // Act
        var act = () => new ProductImage(imageUrl, alt, title);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Theory]
    [MemberData(nameof(EqualImage))]
    public void ShouldBeEqualImage(ProductImage? image1, ProductImage? image2, string reason)
    {
        // Act
        var result = EqualityComparer<ProductImage>.Default.Equals(image1, image2);

        // Assert
        Assert.True(result, reason);
    }

    [Theory]
    [MemberData(nameof(NonEqualImage))]
    public void ShouldBeNonEqualImage(ProductImage? image1, ProductImage? image2, string reason)
    {
        // Act
        var result = EqualityComparer<ProductImage>.Default.Equals(image1, image2);

        // Assert
        Assert.False(result, reason);
    }
}