using DrugStore.Application.Users.Queries.GetByIdQuery;
using DrugStore.Domain.IdentityAggregate;
using DrugStore.UnitTest.Builders;
using FluentAssertions;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using NSubstitute;

namespace DrugStore.UnitTest.UseCases.UserTests;

public sealed class GetByIdQueryHandlerTest
{
    private readonly GetByIdQueryHandler _handler;

    private readonly IMapper _mapper = Substitute.For<IMapper>();

    private readonly UserManager<ApplicationUser> _userManager =
        Substitute.For<UserManager<ApplicationUser>>(
            Substitute.For<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null
        );

    public GetByIdQueryHandlerTest() => _handler = new(_mapper, _userManager);

    private static ApplicationUser CreateUser() => new(
        "testuser@gmail.com",
        "Test User",
        "1234567890",
        AddressBuilder.WithDefaultValues()
    );

    [Fact]
    public async Task ShouldGetUserSuccessfully()
    {
        // Arrange
        var query = new GetByIdQuery(new(Guid.NewGuid()));
        _userManager.FindByIdAsync(Arg.Any<string>())
            .Returns(CreateUser());

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}