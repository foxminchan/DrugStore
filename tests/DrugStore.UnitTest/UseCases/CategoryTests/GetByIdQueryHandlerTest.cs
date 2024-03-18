using DrugStore.Application.Categories.Queries.GetByIdQuery;
using DrugStore.Domain.CategoryAggregate;
using DrugStore.Domain.CategoryAggregate.Specifications;
using DrugStore.Domain.SharedKernel;
using DrugStore.Infrastructure.Cache.Redis;
using MapsterMapper;
using NSubstitute;

namespace DrugStore.UnitTest.UseCases.CategoryTests;

public sealed class GetByIdQueryHandlerTest
{
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly IRedisService _redisService = Substitute.For<IRedisService>();
    private readonly IReadRepository<Category> _repository = Substitute.For<IReadRepository<Category>>();

    public GetByIdQueryHandlerTest()
    {
        var category = new Category("Category Name", "Category Description");

        _repository.FirstOrDefaultAsync(Arg.Any<CategoryByIdSpec>())
            .Returns(category);
    }

    [Fact]
    public async Task NotBeNullIfCategoryExists()
    {
        // Arrange
        var query = new GetByIdQuery(new(Guid.Empty));
        var handler = new GetByIdQueryHandler(_mapper, _repository, _redisService);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
    }
}