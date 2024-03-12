using Bogus;
using DrugStore.Domain.CategoryAggregate;
using FluentAssertions;

namespace DrugStore.UnitTest.Domains.CategoryTests;

public sealed class CategoryAggregateTest(ITestOutputHelper output)
{
    private readonly Faker _faker = new();

    [Fact]
    public void ShouldBeUpdateCategorySuccessfully()
    {
        // Arrange
        var category = new Category(_faker.Commerce.Categories(1)[0], _faker.Lorem.Sentence());
        var title = _faker.Commerce.Categories(1)[0];
        var description = _faker.Lorem.Sentence();

        // Act
        category.Update(title, description);

        // Assert
        category.Name.Should().Be(title);
        category.Description.Should().Be(description);
        output.WriteLine("Category: {0}", category);
    }

    [Fact]
    public void ShouldBeThrowExceptionWhenUpdateCategoryWithEmptyTitle()
    {
        // Arrange
        var category = new Category(_faker.Commerce.Categories(1)[0], _faker.Lorem.Sentence());
        var title = string.Empty;
        var description = _faker.Lorem.Sentence();

        // Act
        var act = () => category.Update(title, description);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void ShouldBeInitializeCategorySuccessfully()
    {
        // Arrange
        var title = _faker.Commerce.Categories(1)[0];
        var description = _faker.Lorem.Sentence();

        // Act
        var category = new Category(title, description);

        // Assert
        category.Name.Should().Be(title);
        category.Description.Should().Be(description);
        output.WriteLine("Category: {0}", category);
    }

    [Fact]
    public void ShouldBeThrowExceptionWhenInitializeCategoryWithEmptyTitle()
    {
        // Arrange
        var title = string.Empty;
        var description = _faker.Lorem.Sentence();

        // Act
        var act = () => new Category(title, description);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

}