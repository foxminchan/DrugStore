using DrugStore.Domain.CategoryAggregate;
using DrugStore.Domain.CategoryAggregate.Primitives;
using DrugStore.Domain.CategoryAggregate.Specifications;

namespace DrugStore.UnitTest.Domains.CategoryTests.Specifications;

public sealed class CategoryByIdSpecTest
{
    private readonly CategoryId _id = new(new("1d6f3b3e-3e3d-4e6e-8b3e-3f3e3d1d6f3b"));

    [Fact]
    public void MatchesCategoryWithGivenCategoryId()
    {
        // Arrange
        var spec = new CategoryByIdSpec(_id);

        // Act
        var result = spec.Evaluate(GetTestCategoryCollection()).FirstOrDefault();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(_id, result.Id);
    }

    [Fact]
    public void MatchesNoCategoryIfCategoryIdIsNotPresent()
    {
        // Arrange
        var id = new CategoryId(Guid.NewGuid());
        var spec = new CategoryByIdSpec(id);

        // Act
        var result = spec.Evaluate(GetTestCategoryCollection()).FirstOrDefault();

        // Assert
        Assert.Null(result);
    }

    private IEnumerable<Category> GetTestCategoryCollection() =>
    [
        new()
        {
            Id = _id,
            Name = "Category 1",
            Description = "Category 1 description"
        },
        new()
        {
            Id = new(Guid.NewGuid()),
            Name = "Category 2",
            Description = "Category 2 description"
        },
        new()
        {
            Id = new(Guid.NewGuid()),
            Name = "Category 3",
            Description = "Category 3 description"
        }
    ];
}