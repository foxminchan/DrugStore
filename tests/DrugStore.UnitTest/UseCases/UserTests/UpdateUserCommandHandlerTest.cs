using DrugStore.Application.Users.Commands.UpdateUserCommand;
using DrugStore.Domain.IdentityAggregate;
using DrugStore.Domain.IdentityAggregate.ValueObjects;
using FluentAssertions;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace DrugStore.UnitTest.UseCases.UserTests;

public sealed class UpdateUserCommandHandlerTest
{
    private readonly UpdateUserCommandHandler _handler;

    private readonly ILogger<UpdateUserCommandHandler> _logger = Substitute.For<ILogger<UpdateUserCommandHandler>>();

    private readonly IMapper _mapper = Substitute.For<IMapper>();

    private readonly UserManager<ApplicationUser> _userManager =
        Substitute.For<UserManager<ApplicationUser>>(
            Substitute.For<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null
        );

    public UpdateUserCommandHandlerTest() => _handler = new(_mapper, _userManager, _logger);

    [Fact]
    public async Task ShouldUpdateUserSuccessfully()
    {
        // Arrange
        const string email = "test@gmail.com";
        const string fullname = "Test User";
        const string phoneNumber = "1234567890";
        const string password = "Test@123";
        var address = new Address("Test Street", "Test City", "Test Province");
        var command = new UpdateUserCommand(new(Guid.NewGuid()), email, password, password, password, fullname,
            phoneNumber, address);
        _userManager.UpdateAsync(Arg.Any<ApplicationUser>())
            .Returns(IdentityResult.Success);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}