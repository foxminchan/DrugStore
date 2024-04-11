using DrugStore.Application.Categories.Commands.UpdateCategoryCommand;
using DrugStore.Domain.CategoryAggregate;
using DrugStore.Domain.CategoryAggregate.Specifications;
using DrugStore.Persistence.Repositories;
using FluentAssertions;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace DrugStore.UnitTest.UseCases.CategoryTests;

public sealed class UpdateCategoryCommandHandlerTest
{
    private readonly UpdateCategoryCommandHandler _handler;

    private readonly ILogger<UpdateCategoryCommandHandler> _logger =
        Substitute.For<ILogger<UpdateCategoryCommandHandler>>();

    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly IRepository<Category> _repository = Substitute.For<IRepository<Category>>();

    public UpdateCategoryCommandHandlerTest() => _handler = new(_mapper, _repository, _logger);

    private static Category CreateCategory() => new("Category Name", "Category Description");

    [Fact]
    public async Task ShouldBeUpdateCategorySuccessfully()
    {
        // Arrange
        var command = new UpdateCategoryCommand(
            new(Guid.NewGuid()),
            "Category Name",
            "Category Description"
        );
        _repository.FirstOrDefaultAsync(Arg.Any<CategoryByIdSpec>())!
            .Returns(Task.FromResult(CreateCategory()));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}