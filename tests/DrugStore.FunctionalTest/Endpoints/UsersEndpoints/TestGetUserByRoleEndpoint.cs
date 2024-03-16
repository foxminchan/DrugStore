using System.Net.Http.Json;
using DrugStore.FunctionalTest.Extensions;
using DrugStore.FunctionalTest.Fakers;
using DrugStore.FunctionalTest.Fixtures;
using DrugStore.WebAPI.Endpoints.User;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace DrugStore.FunctionalTest.Endpoints.UsersEndpoints;

public sealed class TestGetUserByRoleEndpoint(ApplicationFactory<Program> factory, ITestOutputHelper output)
    : IClassFixture<ApplicationFactory<Program>>, IAsyncLifetime
{
    private readonly ApplicationFactory<Program> _factory = factory.WithDbContainer();

    public async Task InitializeAsync() => await _factory.StartContainersAsync();

    public async Task DisposeAsync() => await _factory.StopContainersAsync();

    [Fact]
    public async Task ShouldBeReturnUsersByRole()
    {
        // Arrange
        var client = _factory.CreateClient();
        var users = new ApplicationUserFaker().Generate(10);

        // Act
        await _factory.EnsureCreatedAndPopulateDataAsync(users);
        var response = await client.GetAsync($"/api/v1/users/isStaff/true");

        // Assert
        response.EnsureSuccessStatusCode();
        var items = await response.Content.ReadFromJsonAsync<List<UserDto>>();
        output.WriteLine("Response: {0}", items);
        items.Should()
            .NotBeNull();
    }
}