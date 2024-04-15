using System.Net;
using System.Net.Http.Json;
using DrugStore.FunctionalTest.Extensions;
using DrugStore.FunctionalTest.Fakers;
using DrugStore.FunctionalTest.Fixtures;
using DrugStore.WebAPI.Endpoints.Basket;
using FluentAssertions;

namespace DrugStore.FunctionalTest.Endpoints.BasketEndpoints;

public sealed class TestGetBasketByIdEndpoint(ApplicationFactory<Program> factory, ITestOutputHelper output)
    : IClassFixture<ApplicationFactory<Program>>, IAsyncLifetime
{
    private readonly ApplicationFactory<Program> _factory = factory.WithCacheContainer();

    private readonly BasketFaker _faker = new();

    public async Task InitializeAsync() => await _factory.StartContainersAsync();

    public async Task DisposeAsync() => await _factory.StopContainersAsync();

    [Fact]
    public async Task ShouldBeReturnBasket()
    {
        // Arrange
        var client = _factory.CreateClient();
        var basket = _faker.Generate(1);
        var id = basket[0].Id;

        // Act
        await _factory.EnsureCreatedAndPopulateDataAsync(basket);
        var response = await client.GetAsync($"/api/v1/baskets/{id}");

        // Assert
        response.EnsureSuccessStatusCode();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var item = await response.Content.ReadFromJsonAsync<CustomerBasketDto>();
        output.WriteLine("Response: {0}", item);
        item.Should().NotBeNull().And.Match<CustomerBasketDto>(x => x.Id == id);
    }

    [Theory(DisplayName = "Should be return not found")]
    [InlineData("00000000-0000-0000-0000-000000000000")]
    [InlineData("00000000-0000-0000-0000-000000000001")]
    public async Task ShouldBeReturnNotFound(string id)
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync($"/api/v1/baskets/{id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        output.WriteLine("Response: {0}", response);
    }
}