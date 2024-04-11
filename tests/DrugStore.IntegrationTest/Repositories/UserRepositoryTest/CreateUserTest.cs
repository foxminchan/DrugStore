using System.Text.Json;
using DrugStore.Domain.IdentityAggregate;
using DrugStore.Domain.IdentityAggregate.ValueObjects;
using DrugStore.IntegrationTest.Fixtures;
using Microsoft.AspNetCore.Identity;
using NSubstitute;

namespace DrugStore.IntegrationTest.Repositories.UserRepositoryTest;

public sealed class CreateUserTest(ITestOutputHelper output) : BaseEfRepoTestFixture
{
    private readonly UserManager<ApplicationUser> _userManager =
        Substitute.For<UserManager<ApplicationUser>>(
            Substitute.For<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null
        );

    [Fact]
    public async Task ShouldCreateUser()
    {
        // Arrange
        const string email = "test@gmail.com";
        const string fullname = "Test User";
        const string phoneNumber = "1234567890";
        const string password = "Test@123";
        var address = new Address("Test Street", "Test City", "Test Province");
        var user = new ApplicationUser(email, fullname, phoneNumber, address);

        // Act
        output.WriteLine("User: " + JsonSerializer.Serialize(user));
        var created = await _userManager.CreateAsync(user, password);

        // Assert
        Assert.NotNull(created);
    }

    [Theory]
    [ClassData(typeof(InvalidData))]
    public async Task ShouldNotCreateUser(ApplicationUser user)
    {
        // Arrange
        const string password = "Test@123";

        // Act
        output.WriteLine("User: " + JsonSerializer.Serialize(user));
        var created = await _userManager.CreateAsync(user, password);

        // Assert
        Assert.False(created.Succeeded);
    }
}

internal class InvalidData : TheoryData<ApplicationUser>
{
    public InvalidData()
    {
        Add(new(string.Empty, "Test User", "1234567890", new("Test Street", "Test City", "Test Province")));
        Add(new("test@gmail.com", string.Empty, "1234567890", new("Test Street", "Test City", "Test Province")));
        Add(new("test@gmail.com", "Test User", string.Empty, new("Test Street", "Test City", "Test Province")));
    }
}