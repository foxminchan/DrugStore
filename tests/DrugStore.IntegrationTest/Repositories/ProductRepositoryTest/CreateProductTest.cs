using System.Text.Json;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.ProductAggregate.ValueObjects;
using DrugStore.IntegrationTest.Fixtures;
using DrugStore.Persistence;

namespace DrugStore.IntegrationTest.Repositories.ProductRepositoryTest;

public sealed class CreateProductTest : BaseEfRepoTestFixture
{
    private readonly ITestOutputHelper _output;
    private readonly Repository<Product> _repository;

    public CreateProductTest(ITestOutputHelper output)
    {
        _output = output;
        _repository = new(DbContext);
    }

    [Fact]
    public async Task ShouldCreateProduct()
    {
        // Arrange
        const string name = "Product 1";
        const string code = "P001";
        const string detail = "Product 1 Description";
        const int quantity = 10;
        ProductPrice price = new(100, 90);
        var product = new Product(name, code, detail, quantity, null, price);

        // Act
        var created = await _repository.AddAsync(product);
        _output.WriteLine("Product: " + JsonSerializer.Serialize(product));

        // Assert
        Assert.NotNull(created);
        Assert.Equal(product.Name, created.Name);
        Assert.Equal(product.ProductCode, created.ProductCode);
        Assert.Equal(product.Detail, created.Detail);
        Assert.Equal(product.Quantity, created.Quantity);
        Assert.Equal(product.Price, created.Price);
    }
}