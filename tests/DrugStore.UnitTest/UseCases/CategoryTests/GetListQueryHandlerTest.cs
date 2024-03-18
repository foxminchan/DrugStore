using Bogus;
using DrugStore.Application.Categories.Queries.GetListQuery;
using DrugStore.Domain.CategoryAggregate;
using DrugStore.Domain.SharedKernel;
using MapsterMapper;
using NSubstitute;

namespace DrugStore.UnitTest.UseCases.CategoryTests;

public sealed class GetListQueryHandlerTest
{
    private readonly Faker _faker = new();
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly IReadRepository<Category> _repository = Substitute.For<IReadRepository<Category>>();

    public GetListQueryHandlerTest()
    {
        var categories = new List<Category>
        {
            new(_faker.Commerce.Categories(1)[0], _faker.Lorem.Sentence()),
            new(_faker.Commerce.Categories(1)[0], _faker.Lorem.Sentence()),
            new(_faker.Commerce.Categories(1)[0], _faker.Lorem.Sentence()),
        };

        _repository.ListAsync(Arg.Any<CancellationToken>())
            .Returns(categories);
    }

    [Fact]
    public async Task NotBeNullIfCategoryExists()
    {
        // Arrange
        var query = new GetListQuery();
        var handler = new GetListQueryHandler(_mapper, _repository);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
    }
}