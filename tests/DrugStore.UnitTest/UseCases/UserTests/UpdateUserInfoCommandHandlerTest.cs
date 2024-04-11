using DrugStore.Application.Users.Commands.UpdateUserInfoCommand;
using DrugStore.Domain.IdentityAggregate;
using DrugStore.Domain.IdentityAggregate.Constants;
using DrugStore.Domain.IdentityAggregate.ValueObjects;
using FluentAssertions;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace DrugStore.UnitTest.UseCases.UserTests;

public sealed class UpdateUserInfoCommandHandlerTest
{
    private readonly UpdateUserInfoCommandHandler _handler;

    private readonly ILogger<UpdateUserInfoCommandHandler> _logger =
        Substitute.For<ILogger<UpdateUserInfoCommandHandler>>();

    private readonly IMapper _mapper = Substitute.For<IMapper>();

    private readonly UserManager<ApplicationUser> _userManager =
        Substitute.For<UserManager<ApplicationUser>>(
            Substitute.For<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null
        );

    public UpdateUserInfoCommandHandlerTest() => _handler = new(_mapper, _userManager, _logger);

    [Fact]
    public async Task ShouldUpdateUserSuccessfully()
    {
        // Arrange
        const string email = "test@gmail.com";
        const string fullname = "Test User";
        const string phoneNumber = "1234567890";
        var address = new Address("Test Street", "Test City", "Test Province");
        var command =
            new UpdateUserInfoCommand(new(Guid.NewGuid()), email, fullname, phoneNumber, address, Roles.ADMIN);
        _userManager.UpdateAsync(Arg.Any<ApplicationUser>())
            .Returns(IdentityResult.Success);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}