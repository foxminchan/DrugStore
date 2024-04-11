using DrugStore.Application.Users.Commands.CreateUserCommand;
using DrugStore.Domain.IdentityAggregate;
using DrugStore.UnitTest.Builders;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace DrugStore.UnitTest.UseCases.UserTests;

public sealed class CreateUserCommandHandlerTest
{
    private readonly CreateUserCommandHandler _handler;

    private readonly ILogger<CreateUserCommandHandler> _logger = Substitute.For<ILogger<CreateUserCommandHandler>>();

    private readonly UserManager<ApplicationUser> _userManager =
        Substitute.For<UserManager<ApplicationUser>>(
            Substitute.For<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null
        );

    public CreateUserCommandHandlerTest() => _handler = new(_userManager, _logger);

    [Fact]
    public async Task ShouldCreateUser()
    {
        // Arrange
        const string email = "test@gmail.com";
        const string fullname = "Test User";
        const string phoneNumber = "1234567890";
        const string password = "Test@123";
        var command = new CreateUserCommand(Guid.NewGuid(), email, password, password, fullname, phoneNumber,
            AddressBuilder.WithDefaultValues(), false);
        _userManager.CreateAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>())
            .Returns(IdentityResult.Success);

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

internal sealed class InvalidData : TheoryData<CreateUserCommand>
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