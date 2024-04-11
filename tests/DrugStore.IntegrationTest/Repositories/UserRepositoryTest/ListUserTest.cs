using System.Text.Json;
using DrugStore.Domain.IdentityAggregate;
using DrugStore.IntegrationTest.Fixtures;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NSubstitute;

namespace DrugStore.IntegrationTest.Repositories.UserRepositoryTest;

public sealed class ListUserTest(ITestOutputHelper output) : BaseEfRepoTestFixture
{
    private readonly UserManager<ApplicationUser> _userManager =
        Substitute.For<UserManager<ApplicationUser>>(
            Substitute.For<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null
        );

    [Fact]
    public async Task ShouldListUser()
    {
        // Arrange
        var users = new List<ApplicationUser>()
        {
            new("testuser1@gmail.com", "Test User 1", "1234567890",
                new("Test Street 1", "Test City 1", "Test Province 1")),
            new("testuser2@gmail.com", "Test User 2", "1234567890",
                new("Test Street 2", "Test City 2", "Test Province 2")),
            new("testuser3@gmail.com", "Test User 3", "1234567890",
                new("Test Street 3", "Test City 3", "Test Province 3"))
        };

        foreach (var user in users)
        {
            await _userManager.CreateAsync(user);
            output.WriteLine("User: " + JsonSerializer.Serialize(user));
        }

        // Act
        var result = await _userManager.Users.ToListAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(users.Count, result.Count);
    }

    [Fact]
    public async Task ShouldListEmptyUser()
    {
        // Act
        var result = await _userManager.Users.ToListAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}