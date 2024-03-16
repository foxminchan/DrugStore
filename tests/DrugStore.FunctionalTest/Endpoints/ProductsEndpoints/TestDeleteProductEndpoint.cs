using System.Net;
using DrugStore.FunctionalTest.Extensions;
using DrugStore.FunctionalTest.Fakers;
using DrugStore.FunctionalTest.Fixtures;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace DrugStore.FunctionalTest.Endpoints.ProductsEndpoints;

public sealed class TestDeleteProductEndpoint(ApplicationFactory<Program> factory, ITestOutputHelper output)
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
        var response = await client.DeleteAsync($"/api/v1/products/{id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        output.WriteLine("Response: {0}", response);
    }

    [Theory(DisplayName = "Should be return not found")]
    [InlineData("00000000-0000-0000-0000-000000000000")]
    [InlineData("00000000-0000-0000-0000-000000000001")]
    public async Task ShouldBeReturnNotFound(string id)
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.DeleteAsync($"/api/v1/products/{id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        output.WriteLine("Response: {0}", response);
    }
}