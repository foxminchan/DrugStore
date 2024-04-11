using DrugStore.Application.Users.Commands.ResetPasswordCommand;
using DrugStore.Domain.IdentityAggregate;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using NSubstitute;

namespace DrugStore.UnitTest.UseCases.UserTests;

public class ResetPasswordCommandHandlerTest
{
    private readonly ResetPasswordCommandHandler _handler;

    private readonly UserManager<ApplicationUser> _userManager =
        Substitute.For<UserManager<ApplicationUser>>(
            Substitute.For<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null
        );

    public ResetPasswordCommandHandlerTest() => _handler = new(_userManager);

    [Fact]
    public async Task ShouldResetPassword()
    {
        // Arrange
        var command = new ResetPasswordCommand(new(Guid.NewGuid()));
        _userManager.ResetPasswordAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>(), Arg.Any<string>())
            .Returns(IdentityResult.Success);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}