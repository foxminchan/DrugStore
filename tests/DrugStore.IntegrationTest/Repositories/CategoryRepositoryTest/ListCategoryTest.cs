using System.Text.Json;
using DrugStore.Domain.CategoryAggregate;
using DrugStore.IntegrationTest.Fixtures;
using DrugStore.Persistence.Repositories;

namespace DrugStore.IntegrationTest.Repositories.CategoryRepositoryTest;

public sealed class ListCategoryTest : BaseEfRepoTestFixture
{
    private readonly ITestOutputHelper _output;
    private readonly Repository<Category> _repository;

    public ListCategoryTest(ITestOutputHelper output)
    {
        _output = output;
        _repository = new(DbContext);
    }

    [Fact]
    public async Task ShouldListCategory()
    {
        // Arrange
        var categories = new List<Category>
        {
            new("Category 1", "Category 1 Description"),
            new("Category 2", "Category 2 Description"),
            new("Category 3", "Category 3 Description")
        };
        await _repository.AddRangeAsync(categories);
        _output.WriteLine("Categories: " + JsonSerializer.Serialize(categories));

        // Act
        var list = await _repository.ListAsync();

        // Assert
        Assert.NotNull(list);
        Assert.Equal(categories.Count, list.Count);
    }

    [Fact]
    public async Task ShouldListEmptyCategory()
    {
        // Arrange
        var categories = new List<Category>();
        _output.WriteLine("Categories: " + JsonSerializer.Serialize(categories));

        // Act
        var list = await _repository.ListAsync();

        // Assert
        Assert.NotNull(list);
        Assert.Equal(categories.Count, list.Count);
    }
}