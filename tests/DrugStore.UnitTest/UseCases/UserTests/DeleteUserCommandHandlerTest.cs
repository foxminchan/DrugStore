﻿using DrugStore.Application.Users.Commands.DeleteUserCommand;
using DrugStore.Domain.IdentityAggregate;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using NSubstitute;

namespace DrugStore.UnitTest.UseCases.UserTests;

public sealed class DeleteUserCommandHandlerTest
{
    private readonly DeleteUserCommandHandler _handler;

    private readonly UserManager<ApplicationUser> _userManager =
        Substitute.For<UserManager<ApplicationUser>>(
            Substitute.For<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null
        );

    public DeleteUserCommandHandlerTest() => _handler = new(_userManager);

    [Fact]
    public async Task ShouldBeDeleteUserSuccessfully()
    {
        // Arrange
        var command = new DeleteUserCommand(new(Guid.NewGuid()));
        _userManager.DeleteAsync(Arg.Any<ApplicationUser>())
            .Returns(IdentityResult.Success);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}