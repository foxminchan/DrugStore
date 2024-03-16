using System.Net;
using System.Net.Http.Json;
using DrugStore.FunctionalTest.Extensions;
using DrugStore.FunctionalTest.Fakers;
using DrugStore.FunctionalTest.Fixtures;
using DrugStore.WebAPI.Endpoints.Product;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace DrugStore.FunctionalTest.Endpoints.ProductsEndpoints;

public sealed class TestGetProductsEndpoint(ApplicationFactory<Program> factory, ITestOutputHelper output)
    : IClassFixture<ApplicationFactory<Program>>, IAsyncLifetime
{
    private readonly ApplicationFactory<Program> _factory = factory.WithDbContainer();

    public async Task InitializeAsync() => await _factory.StartContainersAsync();

    public async Task DisposeAsync() => await _factory.StopContainersAsync();

    [Fact]
    public async Task ShouldBeReturnProducts()
    {
        // Arrange
        var client = _factory.CreateClient();
        var products = new ProductFaker().Generate(10);

        // Act
        await _factory.EnsureCreatedAndPopulateDataAsync(products);
        var response = await client.GetAsync("/api/v1/products?pageSize=10&pageIndex=1&search=");

        // Assert
        response.EnsureSuccessStatusCode();
        var items = await response.Content.ReadFromJsonAsync<List<ProductDto>>();
        output.WriteLine("Response: {0}", items);
        items.Should()
            .NotBeNull()
            .And
            .Match<List<ProductDto>>(x => x.Count == 10);
    }

    [Fact]
    public async Task ShouldBeReturnProductsWithSearch()
    {
        // Arrange
        var client = _factory.CreateClient();
        var products = new ProductFaker().Generate(10);
        var search = products[0].Name;

        // Act
        await _factory.EnsureCreatedAndPopulateDataAsync(products);
        var response = await client.GetAsync($"/api/v1/products?pageSize=10&pageIndex=1&search={search}");

        // Assert
        response.EnsureSuccessStatusCode();
        var items = await response.Content.ReadFromJsonAsync<List<ProductDto>>();
        output.WriteLine("Response: {0}", items);
        items.Should()
            .NotBeNull()
            .And
            .Match<List<ProductDto>>(x => x.Count == 1);
    }

    [Fact]
    public async Task ShouldBeInvalidWhenPageSizeIsPositiveAndPageIndexIsZero()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/v1/products?pageSize=10&pageIndex=0&search=");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        output.WriteLine("Response: {0}", response);
    }
}