using System.Text.Json;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.ProductAggregate.ValueObjects;
using DrugStore.IntegrationTest.Fixtures;
using DrugStore.Persistence;

namespace DrugStore.IntegrationTest.Repositories.ProductRepositoryTest;

public sealed class UpdateProductTest : BaseEfRepoTestFixture
{
    private readonly ITestOutputHelper _output;
    private readonly Repository<Product> _repository;

    public UpdateProductTest(ITestOutputHelper output)
    {
        _output = output;
        _repository = new(DbContext);
    }

    [Fact]
    public async Task ShouldUpdateProduct()
    {
        // Arrange
        const string name = "Product 1";
        const string code = "P001";
        const string detail = "Product 1 Description";
        const int quantity = 10;
        ProductPrice price = new(100, 90);
        var product = new Product(name, code, detail, quantity, null, price);
        await _repository.AddAsync(product);
        var createdProduct = await _repository.GetByIdAsync(product.Id) ?? throw new NullReferenceException();
        _output.WriteLine("Product: " + JsonSerializer.Serialize(product));

        // Act
        const string updatedName = "Product 1 Updated";
        const string updatedCode = "P001";
        const string updatedDetail = "Product 1 Description Updated";
        const int updatedQuantity = 20;
        ProductPrice updatedPrice = new(200, 180);
        createdProduct.Update(updatedName, updatedCode, updatedDetail, updatedQuantity, null, updatedPrice);
        await _repository.UpdateAsync(createdProduct);
        var updatedProduct = await _repository.GetByIdAsync(createdProduct.Id) ?? throw new NullReferenceException();

        // Assert
        Assert.NotNull(updatedProduct);
        Assert.Equal(updatedName, updatedProduct.Name);
        Assert.Equal(updatedCode, updatedProduct.ProductCode);
        Assert.Equal(updatedDetail, updatedProduct.Detail);
        Assert.Equal(updatedQuantity, updatedProduct.Quantity);
        Assert.Equal(updatedPrice, updatedProduct.Price);
    }
}