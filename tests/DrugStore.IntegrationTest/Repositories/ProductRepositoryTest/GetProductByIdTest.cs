using System.Text.Json;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.ProductAggregate.ValueObjects;
using DrugStore.IntegrationTest.Fixtures;
using DrugStore.Persistence;

namespace DrugStore.IntegrationTest.Repositories.ProductRepositoryTest;

public sealed class GetProductByIdTest : BaseEfRepoTestFixture
{
    private readonly Repository<Product> _repository;
    private readonly ITestOutputHelper _output;

    public GetProductByIdTest(ITestOutputHelper output)
    {
        _output = output;
        _repository = new(DbContext);
    }

    [Fact]
    public async Task ShouldGetProductById()
    {
        // Arrange
        const string name = "Product 1";
        const string code = "P001";
        const string detail = "Product 1 Description";
        const int quantity = 10;
        ProductPrice price = new(100, 90);
        var product = new Product(name, code, detail, quantity, null, price);
        await _repository.AddAsync(product);
        _output.WriteLine("Product: " + JsonSerializer.Serialize(product));

        // Act
        var getProduct = await _repository.GetByIdAsync(product.Id);

        // Assert
        Assert.NotNull(getProduct);
        Assert.Equal(product.Id, getProduct.Id);
        Assert.Equal(product.Name, getProduct.Name);
        Assert.Equal(product.ProductCode, getProduct.ProductCode);
        Assert.Equal(product.Detail, getProduct.Detail);
        Assert.Equal(product.Quantity, getProduct.Quantity);
        Assert.Equal(product.Price, getProduct.Price);
    }
}