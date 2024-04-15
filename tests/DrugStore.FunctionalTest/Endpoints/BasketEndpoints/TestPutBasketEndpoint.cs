using System.Net;
using System.Net.Http.Json;
using DrugStore.FunctionalTest.Extensions;
using DrugStore.FunctionalTest.Fakers;
using DrugStore.FunctionalTest.Fixtures;
using FluentAssertions;

namespace DrugStore.FunctionalTest.Endpoints.BasketEndpoints;

public sealed class TestPutBasketEndpoint(ApplicationFactory<Program> factory, ITestOutputHelper output)
    : IClassFixture<ApplicationFactory<Program>>, IAsyncLifetime
{
    private readonly ApplicationFactory<Program> _factory = factory.WithCacheContainer();

    private readonly BasketFaker _faker = new();

    public async Task InitializeAsync() => await _factory.StartContainersAsync();

    public async Task DisposeAsync() => await _factory.StopContainersAsync();

    [Fact]
    public async Task ShouldBeReturnOk()
    {
        // Arrange
        var client = _factory.CreateClient();
        var basket = _faker.Generate(1);
        var id = basket[0].Id;

        // Act
        await _factory.EnsureCreatedAndPopulateDataAsync(basket);
        var response = await client.PutAsJsonAsync($"/api/v1/baskets/{id}",
            new { ProductId = Guid.NewGuid(), Quantity = 1 });

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        output.WriteLine("Response: {0}", response);
    }

    [Fact]
    public async Task ShouldBeReturnBadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();
        var basket = _faker.Generate(1);
        var id = basket[0].Id;

        // Act
        await _factory.EnsureCreatedAndPopulateDataAsync(basket);
        var response = await client.PutAsJsonAsync($"/api/v1/baskets/{id}",
            new { ProductId = Guid.Empty, Quantity = 0 });

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
        var response = await client.PutAsJsonAsync($"/api/v1/baskets/{Guid.NewGuid()}",
            new { ProductId = Guid.NewGuid(), Quantity = 1 });

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        output.WriteLine("Response: {0}", response);
    }
}