using DrugStore.Application.Users.Queries.GetListQuery;
using DrugStore.Domain.IdentityAggregate;
using DrugStore.Domain.IdentityAggregate.Constants;
using DrugStore.UnitTest.Builders;
using FluentAssertions;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using NSubstitute;

namespace DrugStore.UnitTest.UseCases.UserTests;

public sealed class GetListQueryHandlerTest
{
    private readonly GetListQueryHandler _handler;

    private readonly IMapper _mapper = Substitute.For<IMapper>();

    private readonly UserManager<ApplicationUser> _userManager =
        Substitute.For<UserManager<ApplicationUser>>(
            Substitute.For<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null
        );

    private readonly List<ApplicationUser> _users;

    public GetListQueryHandlerTest()
    {
        _users =
        [
            new(
                "testuser1@gmail.com",
                "Test User 1",
                "1234567890",
                AddressBuilder.WithDefaultValues()
            ),
            new(
                "testuser2@gmail.com",
                "Test User 2",
                "1234567890",
                AddressBuilder.WithDefaultValues()
            ),
            new(
                "testuser3@gmail.com",
                "Test User 3",
                "1234567890",
                AddressBuilder.WithDefaultValues()
            ),
            new(
                "testuser4@gmail.com",
                "Test User 4",
                "1234567890",
                AddressBuilder.WithDefaultValues()
            ),
            new(
                "testuser5@gmail.com",
                "Test User 5",
                "1234567890",
                AddressBuilder.WithDefaultValues()
            )
        ];
        _handler = new(_mapper, _userManager);
    }

    [Fact]
    public async Task ShouldGetUsersSuccessfully()
    {
        // Arrange
        var query = new GetListQuery(new("Test User 4"), Roles.ADMIN);
        _userManager.Users.Returns(_users.AsQueryable());

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}