using System.Text.Json;
using DrugStore.Domain.IdentityAggregate;
using DrugStore.Domain.IdentityAggregate.ValueObjects;
using DrugStore.IntegrationTest.Fixtures;
using Microsoft.AspNetCore.Identity;
using NSubstitute;

namespace DrugStore.IntegrationTest.Repositories.UserRepositoryTest;

public sealed class UpdateUserInfoTest(ITestOutputHelper output) : BaseEfRepoTestFixture
{
    private readonly UserManager<ApplicationUser> _userManager =
        Substitute.For<UserManager<ApplicationUser>>(
            Substitute.For<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null
        );

    [Fact]
    public async Task ShouldUpdateUserInfo()
    {
        // Arrange
        const string email = "test@gmail.com";
        const string fullname = "Test User";
        const string phoneNumber = "1234567890";
        var address = new Address("Test Street", "Test City", "Test Province");
        var user = new ApplicationUser(email, fullname, phoneNumber, address);
        await _userManager.CreateAsync(user);
        output.WriteLine("User: " + JsonSerializer.Serialize(user));

        // Act
        const string newFullname = "New Test User";
        const string newPhoneNumber = "0987654321";
        var newUser = new ApplicationUser(email, newFullname, newPhoneNumber, address);
        output.WriteLine("New User: " + JsonSerializer.Serialize(newUser));
        var result = await _userManager.UpdateAsync(newUser);

        // Assert
        Assert.NotNull(result);
    }
}