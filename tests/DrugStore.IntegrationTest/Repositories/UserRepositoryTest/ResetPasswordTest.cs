using System.Text.Json;
using DrugStore.Domain.IdentityAggregate;
using DrugStore.Domain.IdentityAggregate.ValueObjects;
using DrugStore.IntegrationTest.Fixtures;
using Microsoft.AspNetCore.Identity;
using NSubstitute;

namespace DrugStore.IntegrationTest.Repositories.UserRepositoryTest;

public sealed class ResetPasswordTest(ITestOutputHelper output) : BaseEfRepoTestFixture
{
    private readonly UserManager<ApplicationUser> _userManager =
        Substitute.For<UserManager<ApplicationUser>>(
            Substitute.For<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null
        );

    [Fact]
    public async Task ShouldResetPassword()
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
        const string newPassword = "NewTest@123";
        var result = await _userManager.ResetPasswordAsync(user,
            await _userManager.GeneratePasswordResetTokenAsync(user), newPassword);
        output.WriteLine("User: " + JsonSerializer.Serialize(result));

        // Assert
        Assert.NotNull(result);
    }
}