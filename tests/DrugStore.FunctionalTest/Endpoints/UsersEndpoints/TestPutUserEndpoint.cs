using System.Net;
using System.Net.Http.Json;
using DrugStore.FunctionalTest.Extensions;
using DrugStore.FunctionalTest.Fakers;
using DrugStore.FunctionalTest.Fixtures;
using FluentAssertions;

namespace DrugStore.FunctionalTest.Endpoints.UsersEndpoints;

public sealed class TestPutUserEndpoint(ApplicationFactory<Program> factory, ITestOutputHelper output)
    : IClassFixture<ApplicationFactory<Program>>, IAsyncLifetime
{
    private readonly ApplicationFactory<Program> _factory = factory.WithDbContainer();

    private readonly ApplicationUserFaker _faker = new();

    public async Task InitializeAsync() => await _factory.StartContainersAsync();

    public async Task DisposeAsync() => await _factory.StopContainersAsync();

    [Fact]
    public async Task ShouldBeReturnOk()
    {
        // Arrange
        var client = _factory.CreateClient();
        var user = _faker.Generate(1);
        var id = user[0].Id;

        // Act
        await _factory.EnsureCreatedAndPopulateDataAsync(user);
        var response = await client.PutAsJsonAsync($"/api/v1/users/{id}",
            new
            {
                Email = "abc@gmail.com",
                FullName = "Test User",
                Password = "P@ssw0rd",
                ConfirmPassword = "P@ssw0rd",
                PhoneNumber = "1234567890",
                Address = new
                {
                    Street = "Vo Van Ngan",
                    City = "Thu Duc",
                    Province = "Ho Chi Minh"
                }
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
        var user = _faker.Generate(1);
        var id = user[0].Id;

        // Act
        await _factory.EnsureCreatedAndPopulateDataAsync(user);
        var response = await client.PutAsJsonAsync($"/api/v1/users/{id}",
            new
            {
                Email = "abc@gmail.com",
                FullName = "Test User",
                Password = "P@sswrd",
                ConfirmPassword = "P@ssw0rd",
                PhoneNumber = "1234567890",
                Address = new
                {
                    Street = "Vo Van Ngan",
                    City = "Thu Duc",
                    Province = "Ho Chi Minh"
                }
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
        var response = await client.PutAsJsonAsync($"/api/v1/users/{Guid.NewGuid()}",
            new
            {
                Email = "abc@gmail.com",
                FullName = "Test User",
                Password = "P@sswrd",
                ConfirmPassword = "P@ssw0rd",
                PhoneNumber = "1234567890",
                Address = new
                {
                    Street = "Vo Van Ngan",
                    City = "Thu Duc",
                    Province = "Ho Chi Minh"
                }
            });

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        output.WriteLine("Response: {0}", response);
    }
}