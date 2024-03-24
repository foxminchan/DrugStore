using System.Text.Json;
using DrugStore.Domain.CategoryAggregate;
using DrugStore.IntegrationTest.Fixtures;
using DrugStore.Persistence;

namespace DrugStore.IntegrationTest.Repositories.CategoryRepositoryTest;

public sealed class UpdateCategoryTest : BaseEfRepoTestFixture
{
    private readonly ITestOutputHelper _output;
    private readonly Repository<Category> _repository;

    public UpdateCategoryTest(ITestOutputHelper output)
    {
        _output = output;
        _repository = new(DbContext);
    }

    [Fact]
    public async Task ShouldUpdateCategory()
    {
        // Arrange
        const string name = "Category 1";
        const string description = "Category 1 Description";
        var category = new Category(name, description);
        await _repository.AddAsync(category);
        var createdCategory = await _repository.GetByIdAsync(category.Id) ?? throw new NullReferenceException();
        _output.WriteLine("Category: " + JsonSerializer.Serialize(createdCategory));

        const string updatedName = "Category 1 Updated";
        const string updatedDescription = "Category 1 Description Updated";
        createdCategory.Update(updatedName, updatedDescription);
        _output.WriteLine("Updated Category: " + JsonSerializer.Serialize(createdCategory));

        // Act
        await _repository.UpdateAsync(createdCategory);
        var updatedCategory = await _repository.GetByIdAsync(createdCategory.Id) ?? throw new NullReferenceException();

        // Assert
        Assert.NotNull(updatedCategory);
        Assert.Equal(updatedName, updatedCategory.Name);
        Assert.Equal(updatedDescription, updatedCategory.Description);
    }
}