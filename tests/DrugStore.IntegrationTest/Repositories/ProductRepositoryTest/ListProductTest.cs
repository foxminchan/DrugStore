using System.Text.Json;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.ProductAggregate.Specifications;
using DrugStore.IntegrationTest.Fixtures;
using DrugStore.Persistence;

namespace DrugStore.IntegrationTest.Repositories.ProductRepositoryTest;

public sealed class ListProductTest : BaseEfRepoTestFixture
{
    private readonly Repository<Product> _repository;
    private readonly ITestOutputHelper _output;

    public ListProductTest(ITestOutputHelper output)
    {
        _output = output;
        _repository = new(DbContext);
    }

    [Fact]
    public async Task ShouldListProduct()
    {
        // Arrange
        var products = new List<Product>
        {
            new("Product 1", "P001", "Product 1 Description", 10, null, new (100, 90)),
            new("Product 2", "P002", "Product 2 Description", 20, null, new (200, 190)),
            new("Product 3", "P003", "Product 3 Description", 30, null, new (300, 290))
        };
        await _repository.AddRangeAsync(products);
        _output.WriteLine("Product: " + JsonSerializer.Serialize(products));

        // Act
        var list = await _repository.ListAsync();

        // Assert
        Assert.NotNull(list);
        Assert.Equal(products.Count, list.Count);
    }

    [Fact]
    public async Task ShouldListEmptyProduct()
    {
        // Arrange
        var products = new List<Product>();
        await _repository.AddRangeAsync(products);
        _output.WriteLine("Product: " + JsonSerializer.Serialize(products));

        // Act
        var list = await _repository.ListAsync();

        // Assert
        Assert.NotNull(list);
        Assert.Equal(products.Count, list.Count);
    }

    [Fact]
    public async Task ShouldListProductWithFilter()
    {
        // Arrange
        var products = new List<Product>
        {
            new("Product 1", "P001", "Product 1 Description", 10, null, new (100, 90)),
            new("Product 2", "P002", "Product 2 Description", 20, null, new (200, 190)),
            new("Product 3", "P003", "Product 3 Description", 30, null, new (300, 290))
        };
        var spec = new ProductsFilterSpec(1, 2, true, "Id", "Product 1");
        await _repository.AddRangeAsync(products);
        _output.WriteLine("Product: " + JsonSerializer.Serialize(products));

        // Act
        var list = await _repository.ListAsync(spec);

        // Assert
        Assert.NotNull(list);
        Assert.Single(list);
    }
}