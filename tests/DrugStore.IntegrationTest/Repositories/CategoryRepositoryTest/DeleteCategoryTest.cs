using System.Text.Json;
using DrugStore.Domain.CategoryAggregate;
using DrugStore.IntegrationTest.Fixtures;
using DrugStore.Persistence.Repositories;

namespace DrugStore.IntegrationTest.Repositories.CategoryRepositoryTest;

public sealed class DeleteCategoryTest : BaseEfRepoTestFixture
{
    private readonly ITestOutputHelper _output;
    private readonly Repository<Category> _repository;

    public DeleteCategoryTest(ITestOutputHelper output)
    {
        _output = output;
        _repository = new(DbContext);
    }

    [Fact]
    public async Task ShouldDeleteCategory()
    {
        // Arrange
        const string name = "Category 1";
        const string description = "Category 1 Description";
        var category = new Category(name, description);
        await _repository.AddAsync(category);
        var createdCategory = await _repository.GetByIdAsync(category.Id) ?? throw new NullReferenceException();
        _output.WriteLine("Category: " + JsonSerializer.Serialize(createdCategory));

        // Act
        await _repository.DeleteAsync(createdCategory);

        // Assert
        Assert.DoesNotContain(await _repository.ListAsync(), c => c.Id == createdCategory.Id);
    }
}