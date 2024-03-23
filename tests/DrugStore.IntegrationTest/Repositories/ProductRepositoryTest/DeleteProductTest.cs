﻿using System.Text.Json;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.ProductAggregate.Enums;
using DrugStore.Domain.ProductAggregate.ValueObjects;
using DrugStore.IntegrationTest.Fixtures;
using DrugStore.Persistence;

namespace DrugStore.IntegrationTest.Repositories.ProductRepositoryTest;

public sealed class DeleteProductTest : BaseEfRepoTestFixture
{
    private readonly Repository<Product> _repository;
    private readonly ITestOutputHelper _output;

    public DeleteProductTest(ITestOutputHelper output)
    {
        _output = output;
        _repository = new(DbContext);
    }

    [Fact]
    public async Task ShouldDeleteProduct()
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
        product.SetDiscontinued();
        await _repository.UpdateAsync(product);

        // Assert
        Assert.DoesNotContain(await _repository.ListAsync(), c => c.Status == ProductStatus.Discontinued);
    }
}