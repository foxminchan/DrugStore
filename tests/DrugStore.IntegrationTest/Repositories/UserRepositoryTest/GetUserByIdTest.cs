using System.Text.Json;
using DrugStore.Domain.IdentityAggregate;
using DrugStore.Domain.IdentityAggregate.ValueObjects;
using DrugStore.IntegrationTest.Fixtures;
using Microsoft.AspNetCore.Identity;
using NSubstitute;

namespace DrugStore.IntegrationTest.Repositories.UserRepositoryTest;

public sealed class GetUserByIdTest(ITestOutputHelper output) : BaseEfRepoTestFixture
{
    private readonly UserManager<ApplicationUser> _userManager =
        Substitute.For<UserManager<ApplicationUser>>(
            Substitute.For<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null
        );

    [Fact]
    public async Task ShouldGetUserById()
    {
        // Arrange
        const string email = "test@gmail.com";
        const string fullname = "Test User";
        const string phoneNumber = "1234567890";
        const string password = "Test@123";
        var address = new Address("Test Street", "Test City", "Test Province");
        var user = new ApplicationUser(email, fullname, phoneNumber, address);
        output.WriteLine("User: " + JsonSerializer.Serialize(user));
        await _userManager.CreateAsync(user, password);

        // Act
        var result = await _userManager.FindByIdAsync(user.Id.ToString());
        output.WriteLine("User: " + JsonSerializer.Serialize(result));

        // Assert
        Assert.NotNull(result);
    }
}