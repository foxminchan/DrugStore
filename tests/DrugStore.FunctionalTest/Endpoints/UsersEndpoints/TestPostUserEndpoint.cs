using System.Net;
using System.Net.Http.Json;
using Bogus;
using DrugStore.FunctionalTest.Extensions;
using DrugStore.FunctionalTest.Fixtures;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace DrugStore.FunctionalTest.Endpoints.UsersEndpoints;

public sealed class TestPostUserEndpoint(ApplicationFactory<Program> factory, ITestOutputHelper output)
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
        var response = await client.PostAsJsonAsync("/api/v1/users",
            new
            {
                Email = faker.Internet.Email(),
                FullName = faker.Name.FullName(),
                Password = "P@ssw0rd",
                ConfirmPassword = "P@ssw0rd",
                PhoneNumber = "1234567890",
                Address = new
                {
                    Street = faker.Address.StreetName(),
                    City = faker.Address.City(),
                    Province = faker.Address.State()
                }
            });

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
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
        var response = await client.PostAsJsonAsync("/api/v1/users", data);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        output.WriteLine("Response: {0}", response);
    }
}

internal sealed class InvalidData : TheoryData<object>
{
    public InvalidData()
    {
        Add(new
        {
            Email = string.Empty,
            FullName = string.Empty,
            Password = "P@ssw0rd",
            ConfirmPassword = "P@sw0rd",
            PhoneNumber = "123456789009009",
            Address = new
            {
                Street = string.Empty,
                City = string.Empty,
                Province = string.Empty
            }
        });
        Add(new
        {
            Email = "invalid-email",
            FullName = "Test User",
            Password = "P@ssw0rd",
            ConfirmPassword = "P@ssw0rd",
            PhoneNumber = "1234567890",
            Address = new
            {
                Street = "Cong Hoa",
                City = "Tan Binh",
                Province = "Ho Chi Minh"
            }
        });
        Add(new
        {
            Email = "invalid-email",
            FullName = "Test User",
            Password = "P@ssw0rd",
            ConfirmPassword = string.Empty,
            PhoneNumber = "1234567890",
            Address = new
            {
                Street = "Cong Hoa",
                City = "Tan Binh",
                Province = "Ho Chi Minh"
            }
        });
        Add(new
        {
            Email = "abc@gmail.com",
            FullName = "Test User",
            Password = "password",
            ConfirmPassword = "password",
            PhoneNumber = "1234567890",
            Address = new
            {
                Street = "Cong Hoa",
                City = "Tan Binh",
                Province = "Ho Chi Minh"
            }
        });
    }
}