﻿using System.Text.Json;
using DrugStore.Domain.IdentityAggregate;
using DrugStore.Domain.IdentityAggregate.ValueObjects;
using DrugStore.IntegrationTest.Fixtures;
using Microsoft.AspNetCore.Identity;
using NSubstitute;

namespace DrugStore.IntegrationTest.Repositories.UserRepositoryTest;

public sealed class DeleteUserTest(ITestOutputHelper output) : BaseEfRepoTestFixture
{
    private readonly UserManager<ApplicationUser> _userManager =
        Substitute.For<UserManager<ApplicationUser>>(
            Substitute.For<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null
        );

    [Fact]
    public async Task ShouldDeleteUser()
    {
        // Arrange
        const string email = "test@gmail.com";
        const string fullname = "Test User";
        const string phoneNumber = "1234567890";
        const string password = "Test@123";
        var address = new Address("Test Street", "Test City", "Test Province");
        var user = new ApplicationUser(email, fullname, phoneNumber, address);
        await _userManager.CreateAsync(user, password);

        // Act
        output.WriteLine("User: " + JsonSerializer.Serialize(user));
        var deleted = await _userManager.DeleteAsync(user);

        // Assert
        Assert.NotNull(deleted);
    }
}