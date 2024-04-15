using System.Net;
using DrugStore.FunctionalTest.Fixtures;
using FluentAssertions;

namespace DrugStore.FunctionalTest.Endpoints.HealthCheckEndpoints;

public class TestReadinessEndpoint(ApplicationFactory<Program> factory, ITestOutputHelper output)
    : IClassFixture<ApplicationFactory<Program>>
{
    [Fact]
    public async Task ShouldBeReturnOk()
    {
        try
        {
            // Arrange
            await factory
                .WithDbContainer()
                .WithCacheContainer()
                .StartContainersAsync();

            var client = factory.CreateClient();

            // Act
            var response = await client.GetAsync("/hc");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            output.WriteLine("Response: {0}", response.StatusCode);
        }
        finally
        {
            await factory.StopContainersAsync();
        }
    }

    [Fact]
    public async Task ShouldBeReturnUnhealthy()
    {
        // Arrange
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/hc");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.ServiceUnavailable);
        output.WriteLine("Response: {0}", response.StatusCode);
    }
}