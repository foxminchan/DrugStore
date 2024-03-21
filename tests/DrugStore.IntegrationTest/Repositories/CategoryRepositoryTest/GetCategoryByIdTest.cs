using System.Text.Json;
using DrugStore.Domain.CategoryAggregate;
using DrugStore.IntegrationTest.Fixtures;
using DrugStore.Persistence;

namespace DrugStore.IntegrationTest.Repositories.CategoryRepositoryTest;

public sealed class GetCategoryByIdTest : BaseEfRepoTestFixture
{
    private readonly Repository<Category> _repository;
    private readonly ITestOutputHelper _output;

    public GetCategoryByIdTest(ITestOutputHelper output)
    {
        _output = output;
        _repository = new(DbContext);
    }

    [Fact]
    public async Task ShouldGetCategoryById()
    {
        // Arrange
        const string name = "Category 1";
        const string description = "Category 1 Description";
        var category = new Category(name, description);
        await _repository.AddAsync(category);
        _output.WriteLine("Category: " + JsonSerializer.Serialize(category));

        // Act
        var getCategory = await _repository.GetByIdAsync(category.Id);

        // Assert
        Assert.NotNull(getCategory);
        Assert.Equal(name, getCategory.Name);
        Assert.Equal(description, getCategory.Description);
    }
}