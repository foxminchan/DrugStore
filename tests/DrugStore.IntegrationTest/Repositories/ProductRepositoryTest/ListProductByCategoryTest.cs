using System.Text.Json;
using DrugStore.Domain.CategoryAggregate;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.ProductAggregate.Specifications;
using DrugStore.IntegrationTest.Fixtures;
using DrugStore.Persistence;

namespace DrugStore.IntegrationTest.Repositories.ProductRepositoryTest;

public sealed class ListProductByCategoryTest : BaseEfRepoTestFixture
{
    private readonly Repository<Category> _categoryRepository;
    private readonly ITestOutputHelper _output;
    private readonly Repository<Product> _productRepository;

    public ListProductByCategoryTest(ITestOutputHelper output)
    {
        _output = output;
        _productRepository = new(DbContext);
        _categoryRepository = new(DbContext);
    }

    [Fact]
    public async Task ShouldListProductByCategory()
    {
        // Arrange
        var category = new Category("Category 1", "C001");
        var newCategory = await _categoryRepository.AddAsync(category);
        var products = new List<Product>
        {
            new("Product 1", "P001", "Product 1 Description", 10, newCategory.Id, new(100, 90)),
            new("Product 2", "P002", "Product 2 Description", 20, newCategory.Id, new(200, 190)),
            new("Product 3", "P003", "Product 3 Description", 30, newCategory.Id, new(300, 290))
        };
        await _productRepository.AddRangeAsync(products);
        var spec = new ProductsByCategoryIdSpec(newCategory.Id, 1, 10);

        // Act
        var list = await _productRepository.ListAsync(spec);

        // Assert
        Assert.NotNull(list);
        Assert.Equal(products.Count, list.Count);
    }

    [Fact]
    public async Task ShouldListEmptyProductByCategory()
    {
        // Arrange
        var products = new List<Product>();
        var spec = new ProductsByCategoryIdSpec(new(Guid.NewGuid()), 1, 10);
        await _productRepository.AddRangeAsync(products);
        _output.WriteLine("Product: " + JsonSerializer.Serialize(products));

        // Act
        var list = await _productRepository.ListAsync(spec);

        // Assert
        Assert.NotNull(list);
        Assert.Empty(list);
    }
}