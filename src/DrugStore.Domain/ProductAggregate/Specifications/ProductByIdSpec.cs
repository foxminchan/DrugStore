using Ardalis.Specification;
using DrugStore.Domain.ProductAggregate.Primitives;

namespace DrugStore.Domain.ProductAggregate.Specifications;

public sealed class ProductByIdSpec : Specification<Product>
{
    public ProductByIdSpec(ProductId id) =>
        Query.Where(product => product.Id == id)
            .Include(product => product.Category)
            .EnableCache(nameof(ProductByIdSpec), id);
}