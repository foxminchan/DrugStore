using System.Net;
using System.Net.Http.Json;
using DrugStore.FunctionalTest.Extensions;
using DrugStore.FunctionalTest.Fakers;
using DrugStore.FunctionalTest.Fixtures;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace DrugStore.FunctionalTest.Endpoints.ProductsEndpoints;

public sealed class TestPutProductImageEndpoint(ApplicationFactory<Program> factory, ITestOutputHelper output)
    : IClassFixture<ApplicationFactory<Program>>, IAsyncLifetime
{
    private readonly ApplicationFactory<Program> _factory = factory.WithDbContainer();

    private readonly ProductFaker _faker = new();

    public async Task InitializeAsync() => await _factory.StartContainersAsync();

    public async Task DisposeAsync() => await _factory.StopContainersAsync();

    private static byte[] ImageData => [0x00, 0x01, 0x02, 0x03, 0x04];

    [Fact]
    public async Task ShouldBeReturnOk()
    {
        // Arrange
        var client = _factory.CreateClient();
        var product = _faker.Generate(1);
        var id = product[0].Id;

        // Act
        await _factory.EnsureCreatedAndPopulateDataAsync(product);
        var response = await client.PutAsJsonAsync($"/api/v1/products/image/{id}", new
        {
            Image = ImageData,
            Alt = "New Alt"
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
        var response = await client.PutAsJsonAsync($"/api/v1/products/image/{id}", new
        {
            Image = Array.Empty<byte>(),
            Alt = string.Empty
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
        var response = await client.PutAsJsonAsync($"/api/v1/products/image/{Guid.NewGuid()}", new
        {
            Image = ImageData,
            Alt = "New Alt"
        });

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        output.WriteLine("Response: {0}", response);
    }
}