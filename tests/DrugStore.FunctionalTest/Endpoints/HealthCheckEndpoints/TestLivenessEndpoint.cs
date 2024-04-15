using System.Net;
using DrugStore.FunctionalTest.Fixtures;
using FluentAssertions;

namespace DrugStore.FunctionalTest.Endpoints.HealthCheckEndpoints;

public sealed class TestLivenessEndpoint(ApplicationFactory<Program> factory, ITestOutputHelper output)
    : IClassFixture<ApplicationFactory<Program>>
{
    [Fact]
    public async Task ShouldBeReturnOk()
    {
        // Arrange
        var client = factory
            .WithWebHostBuilder(builder =>
            {
                builder.UseSetting("ConnectionStrings:Postgres",
                    "Host=localhost;Port=5400;Database=test_db;Username=postgres;Password=postgres");
                builder.UseSetting("ConnectionStrings:Redis", "localhost:6379");
            }).CreateClient();

        // Act
        var response = await client.GetAsync("/liveness");

        // Assert
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        Assert.Equal("Healthy", responseString);
        output.WriteLine("Response: {0}", responseString);
    }

    [Theory]
    [InlineData("ConnectionStrings:Redis")]
    [InlineData("ConnectionStrings:Postgres")]
    public async Task ShouldBeReturnUnhealthy(string connectionString)
    {
        // Arrange
        var client = factory
            .WithWebHostBuilder(builder => builder.UseSetting(connectionString, string.Empty)).CreateClient();

        // Act
        var response = await client.GetAsync("/liveness");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.ServiceUnavailable);
        output.WriteLine("Response: {0}", response);
    }
}