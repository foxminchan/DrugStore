using System.Net;
using System.Net.Http.Json;
using Bogus;
using DrugStore.FunctionalTest.Extensions;
using DrugStore.FunctionalTest.Fixtures;
using FluentAssertions;

namespace DrugStore.FunctionalTest.Endpoints.ProductsEndpoints;

public sealed class TestPostProductEndpoint(ApplicationFactory<Program> factory, ITestOutputHelper output)
    : IClassFixture<ApplicationFactory<Program>>, IAsyncLifetime
{
    private readonly ApplicationFactory<Program> _factory = factory.WithDbContainer();

    public async Task InitializeAsync()
    {
        await _factory.StartContainersAsync();
        await _factory.EnsureCreatedAsync();
    }

    public async Task DisposeAsync() => await _factory.StopContainersAsync();

    [Theory]
    [InlineData("a47cb88f-8867-4afe-ba6d-10bbca31e434")] // Change the value to an existing category id
    public async Task ShouldBeReturnCreated(string categoryId)
    {
        // Arrange
        var client = _factory.CreateClient();
        Faker faker = new();

        // Act
        client.DefaultRequestHeaders.Add("X-Idempotency-Key", Guid.NewGuid().ToString());
        var response = await client.PostAsJsonAsync("/api/v1/products",
            new
            {
                Name = faker.Commerce.ProductName(),
                ProductCode = faker.Random.AlphaNumeric(10),
                Detail = faker.Commerce.ProductDescription(),
                Quantity = faker.Random.Int(1, 1000),
                Price = new
                {
                    Price = faker.Random.Decimal(500, 1000),
                    PriceSale = faker.Random.Decimal(1, 500)
                },
                CategoryId = categoryId
            });

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
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
            await client.PostAsJsonAsync("/api/v1/products", data);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        output.WriteLine("Response: {0}", response);
    }

    [Fact]
    public async Task ShouldBeReturnConflict()
    {
        // Arrange
        var client = _factory.CreateClient();
        Faker faker = new();

        // Act
        client.DefaultRequestHeaders.Add("X-Idempotency-Key", Guid.NewGuid().ToString());
        var response = await client.PostAsJsonAsync("/api/v1/products",
            new
            {
                Name = faker.Commerce.ProductName(),
                ProductCode = faker.Random.AlphaNumeric(10),
                Detail = faker.Commerce.ProductDescription(),
                Quantity = faker.Random.Int(1, 1000),
                Price = new
                {
                    Price = faker.Random.Decimal(500, 1000),
                    PriceSale = faker.Random.Decimal(1, 500)
                },
                CategoryId = faker.Random.Guid()
            });

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
        output.WriteLine("Response: {0}", response);
    }
}

internal sealed class InvalidData : TheoryData<object>
{
    public InvalidData()
    {
        Add(new
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
            CategoryId = Guid.Empty
        });
        Add(new
        {
            Name = "New Name",
            ProductCode = "New Code",
            Detail = "New Detail",
            Quantity = -100,
            Price = new
            {
                Price = 1000,
                PriceSale = 500
            },
            CategoryId = Guid.NewGuid()
        });
        Add(new
        {
            Name = "New Name",
            ProductCode = "New Code",
            Detail = "New Detail",
            Quantity = 100,
            Price = new
            {
                Price = 1000,
                PriceSale = -1500
            },
            CategoryId = Guid.Empty
        });
        Add(new
        {
            Name = "New Name",
            ProductCode = "New Code",
            Detail = "New Detail",
            Quantity = 100,
            Price = new
            {
                Price = 1000,
                PriceSale = 1500
            },
            CategoryId = Guid.Empty
        });
    }
}