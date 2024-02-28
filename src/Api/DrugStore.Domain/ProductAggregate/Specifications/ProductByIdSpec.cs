using Ardalis.Specification;

namespace DrugStore.Domain.ProductAggregate.Specifications;

public sealed class ProductByIdSpec : Specification<Product>
{
    public ProductByIdSpec(Guid id)
    {
        Query
            .Where(p => p.Id == id)
            .Include(p => p.Category);
    }
}
