using DrugStore.Application.Products.Queries.GetListByCategoryIdQuery;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.ProductAggregate.Specifications;
using DrugStore.Domain.SharedKernel;
using DrugStore.UnitTest.Builders;
using MapsterMapper;
using NSubstitute;

namespace DrugStore.UnitTest.UseCases.ProductTests;

public sealed class GetListByCategoryIdQueryHandlerTest
{
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly IReadRepository<Product> _repository = Substitute.For<IReadRepository<Product>>();

    private static Product CreateProduct() => new(
        "Product Name",
        "Product Code",
        "Product Detail",
        10,
        new(Guid.NewGuid()),
        ProductPriceBuilder.WithDefaultValues()
    );

    private readonly GetListByCategoryIdQueryHandler _handler;

    public GetListByCategoryIdQueryHandlerTest() => _handler = new(_mapper, _repository);

    [Fact]
    public async Task ShouldBeGetProductSuccessfully()
    {
        // Arrange
        var query = new GetListByCategoryIdQuery(new(Guid.NewGuid()), new());
        _repository.ListAsync(Arg.Any<ProductsByCategoryIdSpec>())
            .Returns(Task.FromResult(new List<Product> { CreateProduct() }));

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
    }
}