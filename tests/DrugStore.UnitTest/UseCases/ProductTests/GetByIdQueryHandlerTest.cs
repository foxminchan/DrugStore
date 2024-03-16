using DrugStore.Application.Products.Queries.GetByIdQuery;
using DrugStore.Domain.ProductAggregate;
using DrugStore.Domain.ProductAggregate.Specifications;
using DrugStore.Domain.SharedKernel;
using DrugStore.UnitTest.Builders;
using NSubstitute;

namespace DrugStore.UnitTest.UseCases.ProductTests;

public sealed class GetByIdQueryHandlerTest
{
    private readonly IReadRepository<Product> _repository = Substitute.For<IReadRepository<Product>>();

    private static Product CreateProduct() => new(
        "Product Name",
        "Product Code",
        "Product Detail",
        10,
        new(Guid.NewGuid()),
        ProductPriceBuilder.WithDefaultValues()
    );

    private readonly GetByIdQueryHandler _handler;

    public GetByIdQueryHandlerTest() => _handler = new(_repository);

    [Fact]
    public async Task ShouldBeGetProductSuccessfully()
    {
        // Arrange
        var query = new GetByIdQuery(new(Guid.NewGuid()));
        _repository.FirstOrDefaultAsync(Arg.Any<ProductByIdSpec>())!
            .Returns(Task.FromResult(CreateProduct()));

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
    }
}