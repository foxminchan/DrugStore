using System.Net;
using System.Net.Http.Json;
using DrugStore.FunctionalTest.Extensions;
using DrugStore.FunctionalTest.Fakers;
using DrugStore.FunctionalTest.Fixtures;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace DrugStore.FunctionalTest.Endpoints.ProductsEndpoints;

public sealed class TestPutProductEndpoint(ApplicationFactory<Program> factory, ITestOutputHelper output)
    : IClassFixture<ApplicationFactory<Program>>, IAsyncLifetime
{
    private readonly ApplicationFactory<Program> _factory = factory.WithDbContainer();

    private readonly ProductFaker _faker = new();

    public async Task InitializeAsync() => await _factory.StartContainersAsync();

    public async Task DisposeAsync() => await _factory.StopContainersAsync();

    [Fact]
    public async Task ShouldBeReturnOk()
    {
        // Arrange
        var client = _factory.CreateClient();
        var product = _faker.Generate(1);
        var id = product[0].Id;

        // Act
        await _factory.EnsureCreatedAndPopulateDataAsync(product);
        var response = await client.PutAsJsonAsync($"/api/v1/products/{id}",
            new
            {
                Name = "New Name",
                ProductCode = "New Code",
                Detail = "New Detail",
                Quantity = 100,
                Price = new
                {
                    Price = 1000,
                    PriceSale = 500
                },
                product[0].CategoryId
            });

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        output.WriteLine("Response: {0}", response);
    }

    [Fact]
    public async Task ShouldBeReturnBadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();
        var product = _faker.Generate(1);
        var id = product[0].Id;

        // Act
        await _factory.EnsureCreatedAndPopulateDataAsync(product);
        var response = await client.PutAsJsonAsync($"/api/v1/products/{id}",
            new
            {
                Name = string.Empty,
                ProductCode = string.Empty,
                Detail = string.Empty,
                Quantity = 0,
                Price = new
                {
                    Price = 0,
                    PriceSale = 0
                },
                product[0].CategoryId
            });

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        output.WriteLine("Response: {0}", response);
    }

    [Fact]
    public async Task ShouldBeReturnNotFound()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.PutAsJsonAsync($"/api/v1/products/{Guid.NewGuid()}",
            new
            {
                Name = "New Name",
                ProductCode = "New Code",
                Detail = "New Detail",
                Quantity = 100,
                Price = new
                {
                    Price = 1000,
                    PriceSale = 500
                },
                CategoryId = Guid.NewGuid()
            });

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        output.WriteLine("Response: {0}", response);
    }

    [Fact]
    public async Task ShouldBeReturnConflict()
    {
        // Arrange
        var client = _factory.CreateClient();
        var product = _faker.Generate(1);
        var id = product[0].Id;

        // Act
        await _factory.EnsureCreatedAndPopulateDataAsync(product);
        var response = await client.PutAsJsonAsync($"/api/v1/products/{id}",
            new
            {
                product[0].Name,
                product[0].ProductCode,
                product[0].Detail,
                product[0].Quantity,
                Price = new
                {
                    product[0].Price?.Price,
                    product[0].Price?.PriceSale
                },
                product[0].CategoryId
            });

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
        output.WriteLine("Response: {0}", response);
    }
}
