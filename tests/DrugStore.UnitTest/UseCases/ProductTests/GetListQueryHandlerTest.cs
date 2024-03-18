using DrugStore.Application.Products.Queries.GetListQuery;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.ProductAggregate.Specifications;
using DrugStore.Domain.SharedKernel;
using DrugStore.UnitTest.Builders;
using MapsterMapper;
using NSubstitute;

namespace DrugStore.UnitTest.UseCases.ProductTests;

public sealed class GetListQueryHandlerTest
{
    private readonly IMapper _mapper = Substitute.For<IMapper>();
    private readonly IReadRepository<Product> _repository = Substitute.For<IReadRepository<Product>>();

    private readonly List<Product> _products;

    private readonly GetListQueryHandler _handler;

    public GetListQueryHandlerTest()
    {
        _products =
        [
            new
            (
                "Product Name 1",
                "Product Code 1",
                "Product Detail 1",
                10,
                new(Guid.NewGuid()),
                ProductPriceBuilder.WithDefaultValues()
            ),

            new
            (
                "Product Name 2",
                "Product Code 2",
                "Product Detail 2",
                20,
                new(Guid.NewGuid()),
                ProductPriceBuilder.WithDefaultValues()
            ),

            new
            (
                "Product Name 3",
                "Product Code 3",
                "Product Detail 3",
                30,
                new(Guid.NewGuid()),
                ProductPriceBuilder.WithDefaultValues()
            )
        ];

        _handler = new(_mapper, _repository);
    }

    [Fact]
    public async Task ShouldBeGetProductSuccessfully()
    {
        // Arrange
        var query = new GetListQuery(new("Product Name 1"));
        _repository.ListAsync(Arg.Any<ProductsFilterSpec>())
            .Returns(Task.FromResult(_products));

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
    }
}