using Ardalis.Specification;
using DrugStore.Domain.CategoryAggregate.Primitives;

namespace DrugStore.Domain.ProductAggregate.Specifications;

public sealed class ProductsByCategoryIdSpec : Specification<Product>
{
    public ProductsByCategoryIdSpec(CategoryId categoryId, int pageNumber, int pageSize)
        => Query.Where(p => p.CategoryId == categoryId)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Include(p => p.Category)
            .EnableCache(nameof(ProductsByCategoryIdSpec), categoryId, pageNumber, pageSize);
}