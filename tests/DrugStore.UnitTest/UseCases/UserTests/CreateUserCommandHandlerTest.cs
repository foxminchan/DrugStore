using DrugStore.Application.Users.Commands.CreateUserCommand;
using DrugStore.Domain.IdentityAggregate;
using DrugStore.Domain.IdentityAggregate.ValueObjects;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace DrugStore.UnitTest.UseCases.UserTests;

public sealed class CreateUserCommandHandlerTest
{
    private readonly UserManager<ApplicationUser> _userManager =
        Substitute.For<UserManager<ApplicationUser>>(
            Substitute.For<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null
        );

    private readonly ILogger<CreateUserCommandHandler> _logger = Substitute.For<ILogger<CreateUserCommandHandler>>();

    private readonly CreateUserCommandHandler _handler;

    public CreateUserCommandHandlerTest() => _handler = new(_userManager, _logger);

    [Fact]
    public async Task ShouldCreateUser()
    {
        // Arrange
        const string email = "test@gmail.com";
        const string fullname = "Test User";
        const string phoneNumber = "1234567890";
        const string password = "Test@123";
        var address = new Address("Test Street", "Test City", "Test Province");
        var command = new CreateUserCommand(Guid.NewGuid(), email, password, password, fullname, phoneNumber, address,
            false);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Theory]
    [ClassData(typeof(InvalidData))]
    public async Task ShouldNotCreateUser(CreateUserCommand command)
    {
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
    }
}

internal class InvalidData : TheoryData<CreateUserCommand>
{
    public InvalidData()
    {
        Add(new(Guid.NewGuid(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
            null, false));
        Add(new(Guid.NewGuid(), string.Empty, "Test@123", "Test@123", "Test User", "1234567890",
            new("Test Street", "Test City", "Test Province"), false));
        Add(new(Guid.NewGuid(), "test@gmail.com", "Test@123", "Test@123", string.Empty, "1234567890",
            new("Test Street", "Test City", "Test Province"), false));
        Add(new(Guid.NewGuid(), "test@gmail.com", "Test@123", "Test@123", "Test User", string.Empty,
            new("Test Street", "Test City", "Test Province"), false));
        Add(new(Guid.NewGuid(), "test@gmail.com", string.Empty, "Test@123", "Test User", "1234567890",
            new("Test Street", "Test City", "Test Province"), false));
        Add(new(Guid.NewGuid(), "test@gmail.com", "Test@123", string.Empty, "Test User", "1234567890",
            new("Test Street", "Test City", "Test Province"), false));
        Add(new(Guid.NewGuid(), "test@gmail.com", "Test@123", "P@ssw0rd", "Test User", "1234567890",
            new("Test Street", "Test City", "Test Province"), false));
        Add(new(Guid.NewGuid(), "test@gmail.com", "Test@123", "P@ssw0rd", "Test User", "1234567890",
            new(string.Empty, "Test City", "Test Province"), false));
        Add(new(Guid.NewGuid(), "test@gamil.com", "Test@123", "Test@123", "Test User", "1234567890",
            new("Test Street", string.Empty, "Test Province"), false));
    }
}