using System.Net;
using System.Net.Http.Json;
using Bogus;
using DrugStore.FunctionalTest.Extensions;
using DrugStore.FunctionalTest.Fixtures;
using FluentAssertions;

namespace DrugStore.FunctionalTest.Endpoints.CategoriesEndpoints;

public sealed class TestPostCategoryEndpoint(ApplicationFactory<Program> factory, ITestOutputHelper output)
    : IClassFixture<ApplicationFactory<Program>>, IAsyncLifetime
{
    private readonly ApplicationFactory<Program> _factory = factory.WithDbContainer();

    public async Task InitializeAsync()
    {
        await _factory.StartContainersAsync();
        await _factory.EnsureCreatedAsync();
    }

    public async Task DisposeAsync() => await _factory.StopContainersAsync();

    [Fact]
    public async Task ShouldBeReturnCreated()
    {
        // Arrange
        var client = _factory.CreateClient();
        Faker faker = new();

        // Act
        client.DefaultRequestHeaders.Add("X-Idempotency-Key", Guid.NewGuid().ToString());
        var response = await client.PostAsJsonAsync("/api/v1/categories",
            new { Name = faker.Commerce.Categories(1)[0], Description = faker.Lorem.Sentence() });

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        response.Headers.Location.Should().NotBeNull();
        output.WriteLine("Response: {0}", response);
    }

    [Theory]
    [ClassData(typeof(InvalidData))]
    public async Task ShouldBeReturnBadRequest(object data)
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        client.DefaultRequestHeaders.Add("X-Idempotency-Key", Guid.NewGuid().ToString());
        var response =
            await client.PostAsJsonAsync("/api/v1/categories", data);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        output.WriteLine("Response: {0}", response);
    }
}

internal class InvalidData : TheoryData<object>
{
    public InvalidData()
    {
        Add(new { Name = string.Empty, Description = "New Description" });
        Add(new { Name = "New Name", Description = string.Empty });
        Add(new { Name = string.Empty, Description = string.Empty });
    }
}