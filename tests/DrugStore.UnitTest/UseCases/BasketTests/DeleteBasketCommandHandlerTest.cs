using DrugStore.Application.Baskets.Commands.DeleteBasketCommand;
using DrugStore.Domain.BasketAggregate;
using DrugStore.Infrastructure.Cache.Redis;
using FluentAssertions;
using NSubstitute;

namespace DrugStore.UnitTest.UseCases.BasketTests;

public sealed class DeleteBasketCommandHandlerTest
{
    private readonly DeleteBasketCommandHandler _handler;
    private readonly IRedisService _redisService = Substitute.For<IRedisService>();

    public DeleteBasketCommandHandlerTest()
    {
        _handler = new(_redisService);

        var basket = new CustomerBasket
        {
            Id = new(Guid.NewGuid()),
            Items =
            [
                new(new(Guid.NewGuid()), "Product Name 1", 1, 10),
                new(new(Guid.NewGuid()), "Product Name 2", 1, 10),
                new(new(Guid.NewGuid()), "Product Name 3", 1, 10)
            ]
        };

        _redisService.Get<CustomerBasket>(Arg.Any<string>())
            .Returns(basket);
    }

    [Fact]
    public async Task ShouldBeDeleteBasketSuccessfully()
    {
        // Arrange
        var command = new DeleteBasketCommand(new(Guid.NewGuid()));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}