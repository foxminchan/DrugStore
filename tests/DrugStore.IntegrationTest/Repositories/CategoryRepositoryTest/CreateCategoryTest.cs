using System.Text.Json;
using DrugStore.Domain.CategoryAggregate;
using DrugStore.IntegrationTest.Fixtures;
using DrugStore.Persistence;

namespace DrugStore.IntegrationTest.Repositories.CategoryRepositoryTest;

public sealed class CreateCategoryTest : BaseEfRepoTestFixture
{
    private readonly Repository<Category> _repository;
    private readonly ITestOutputHelper _output;

    public CreateCategoryTest(ITestOutputHelper output)
    {
        _output = output;
        _repository = new(DbContext);
    }

    [Fact]
    public async Task ShouldCreateCategory()
    {
        // Arrange
        const string name = "Category 1";
        const string description = "Category 1 Description";
        var category = new Category(name, description);
        _output.WriteLine("Category: " + JsonSerializer.Serialize(category));

        // Act
        await _repository.AddAsync(category);
        var createdCategory = await _repository.GetByIdAsync(category.Id);

        // Assert
        Assert.NotNull(createdCategory);
        Assert.Equal(name, createdCategory.Name);
        Assert.Equal(description, createdCategory.Description);
    }
}